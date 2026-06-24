# Business Logic Audit: Payments, Orders, and Inventory

This document lists the business-logic issues that should be corrected before delivery. The goal is not to refactor the whole project, but to protect money, balances, invoices, orders, and inventory from inconsistent states.

## Delivery priority

Fix these first:

1. Add database transactions around money and inventory flows.
2. Remove inventory mutations from validation logic.
3. Correct accounts payable payment update/delete behavior.
4. Stop read operations from mutating accounts receivable state.
5. Fix the accounts receivable filter precedence bug.
6. Confirm and correct invoice tax/ITBIS rules.
7. Add minimal authorization to financial and inventory endpoints.

## Critical issues

### 1. Missing transactions in money and inventory flows

**Risk:** Partial persistence can leave orphan payments, incorrect bank balances, completed expenses without valid payments, or inventory decremented without a valid order.

**Evidence:**

- `TailorProTrack.infraestructure/Repositories/PaymentRepository.cs`
- `TailorProTrack.infraestructure/Repositories/PaymentExpensesRepositoryRepository.cs`
- `TailorProTrack.Application/Extentions/ValidationOrderExtention.cs`
- `TailorProTrack.Application/Service/OrderService.cs`

**Required correction:** Wrap each full business operation in one transaction:

- Accounts receivable payment + bank update + preorder status + credit note.
- Accounts payable payment + bank debit + expense status.
- Order save + order products + inventory decrement.
- Inventory purchase + purchase details + stock increment + generated expense.

**Regression tests:**

- Force an exception after payment creation and verify bank/preorder/credit-note state is rolled back.
- Force an exception after inventory decrement and verify order/inventory state is rolled back.

### 2. Inventory is decremented inside validation

**Risk:** Validation has side effects. If order detail creation fails after validation, inventory remains reduced without a complete order.

**Evidence:**

- `TailorProTrack.Application/Extentions/ValidationOrderExtention.cs`
- `TailorProTrack.Application/Service/OrderService.cs`

**Required correction:** Validation should only validate. Move stock mutation into the order command flow:

1. Validate stock availability.
2. Create order.
3. Create order products.
4. Decrement inventory with a guarded update, for example `WHERE Quantity >= requested`.
5. Recalculate parent inventory totals if applicable.

**Regression tests:**

- Two simultaneous orders for the last available unit: only one should succeed.
- Failure after order creation should roll back order products and inventory changes.

### 3. Accounts payable payment updates bypass bank and expense rules

**Risk:** Updating amount, bank, or expense can leave bank balances and expense completion state incorrect.

**Evidence:**

- `TailorProTrack.Application/Service/PaymentExpenseService.cs`
- `TailorProTrack.Application/Service/BaseServices/GenericService.cs`
- `TailorProTrack.infraestructure/Repositories/PaymentExpensesRepositoryRepository.cs`

**Required correction:** Implement explicit accounts payable payment update logic that:

- Validates pending amount.
- Validates bank balance.
- Recalculates old and new bank accounts when the bank changes.
- Recomputes expense completion status.
- Runs inside a transaction.

**Regression tests:**

- Update a payable payment from bank A to bank B and verify both bank balances.
- Increase a payment above pending amount and verify it fails.
- Reduce a payment for a completed expense and verify the expense becomes pending again.

### 4. Deleting an accounts payable payment does not reopen the expense

**Risk:** A fully paid expense can remain marked as completed after its payment is deleted.

**Evidence:**

- `TailorProTrack.infraestructure/Repositories/PaymentExpensesRepositoryRepository.cs`

**Required correction:** After deleting or updating a payable payment, recalculate the pending amount and set completion state from the recalculated balance.

**Regression test:**

- Fully pay an expense, delete the payment, and verify the expense is no longer completed.

### 5. Financial and inventory APIs lack visible authorization

**Risk:** Anyone who can reach the API may be able to create/delete payments, cancel orders, change inventory, or create invoices.

**Evidence:**

- `TailorProTrack.Api/Program.cs`
- Financial and inventory controllers do not appear to use `[Authorize]`.

**Required correction:** Add authentication and authorization policies/roles, then protect financial and inventory controllers/actions.

## High-priority issues

### 6. Accounts receivable read operation mutates payment status

**Risk:** A GET/reporting operation can unexpectedly change database state, which creates hard-to-debug behavior.

**Evidence:**

- `TailorProTrack.infraestructure/Repositories/PreOrderRepository.cs`
- `TailorProTrack.infraestructure/Repositories/PaymentRepository.cs`

**Required correction:** Separate query logic from command logic. Read operations should compute status for display, not persist changes. Payment status updates should happen only in payment commands or explicit reconciliation jobs.

### 7. Accounts receivable removed-filter precedence bug

**Risk:** Removed preorders with `COMPLETED == false` may appear in accounts receivable.

**Evidence:**

- `TailorProTrack.infraestructure/Repositories/PreOrderRepository.cs`

Current logic:

```csharp
x.REMOVED == false && x.COMPLETED == null || x.COMPLETED == false
```

Corrected logic:

```csharp
x.REMOVED == false && (x.COMPLETED == null || x.COMPLETED == false)
```

### 8. Invoice ITBIS/tax rule is ambiguous

**Risk:** ITBIS may be applied when it should not be, or calculated from the wrong source.

**Evidence:**

- `TailorProTrack.infraestructure/Repositories/SalesRepository.cs`
- `TailorProTrack.Application/Service/SaleService.cs`

Current condition:

```csharp
if(entity.ITBIS != null || entity.ITBIS >= 0)
```

**Required correction:** Confirm the business rule and replace this condition with explicit tax logic. Prefer a clear boolean/rate decision instead of relying on nullable numeric checks.

**Regression tests:**

- Invoice without ITBIS.
- Invoice with ITBIS.
- Invoice where preorder tax configuration differs from sale tax configuration.

## Medium-priority issues

These are important, but can be corrected after the critical delivery risks if time is limited.

- `OrderProductRepository.GetByForeignKeys()` appears to compare reversed fields.
- Deleting inventory purchases may make stock negative if the items were already consumed.
- `BuyInventoryRepository.MarkBuysUsed()` appears to use raw SQL incorrectly and may not save changes.
- Many records hardcode `USER_CREATED = 1`, reducing auditability.
- No migrations were found, so schema constraints, unique indexes, and concurrency protections could not be verified.

## Business rules to confirm

- Should accounts receivable overpayment automatically create a credit note?
- Should cash payments with no bank account affect a cash-box ledger?
- Can a completed invoice/preorder still receive payments or changes?
- Should cancelling a completed order restock inventory?
- Can inventory purchases be deleted after partial consumption?
- What exact rule decides when ITBIS applies?

## Correction checklist

- [ ] Add transactions to AR payment, AP payment, order creation/cancellation, sales, and inventory purchase flows.
- [ ] Move inventory decrements out of validation methods.
- [ ] Implement explicit update logic for accounts payable payments.
- [ ] Recalculate expense completion after payable payment update/delete.
- [ ] Prevent read operations from mutating accounts receivable/payment state.
- [ ] Fix accounts receivable filter parentheses.
- [ ] Require payment `Amount > 0`.
- [ ] Add duplicate-payment protection or idempotency key/reference.
- [ ] Confirm and correct ITBIS business rule.
- [ ] Add authorization to financial and inventory endpoints.
- [ ] Add regression tests for payments, expenses, invoices, orders, stock, and cancellation.

## Suggested implementation order

1. Fix the small correctness bugs first: filter parentheses, `Amount > 0`, and read-side mutation.
2. Add transactions around existing business flows without large refactors.
3. Correct accounts payable update/delete behavior.
4. Refactor order/inventory flow so validation does not mutate stock.
5. Confirm and implement ITBIS rules.
6. Add authorization and regression tests.
