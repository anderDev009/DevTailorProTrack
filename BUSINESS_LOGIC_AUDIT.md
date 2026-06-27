# Business Logic Audit — TailorProTrack Backend

**Audit date:** 2026-06-27  
**Auditor:** Code analysis (fresh-context read-only scan)  
**Scope:** All infrastructure repositories and application services covering payments, orders, inventory, sales, expenses, and credit notes.

---

## Previously reported issues — current status

| Prior Issue | Status |
|---|---|
| Inventory decrement inside validation | **FIXED** |
| Accounts payable payment update/delete: bank balance and expense completion | **FIXED** |
| Accounts receivable read: mutating ConfirmPayment on GET | **FIXED** |
| Accounts receivable filter parentheses bug | **FIXED** |

---

## CRITICAL

### C-1. `OrderProductRepository.GetByForeignKeys()` — FK arguments are swapped

**File:** `TailorProTrack.infraestructure/Repositories/OrderProductRepository.cs:49-53`  
**Risk:** Every `OrderProduct.Update()` call finds the wrong record or throws `InvalidOperationException`. All order-product edits are silently corrupt or crash.

```csharp
// Current (wrong):
.Where(data => data.FK_INVENTORYCOLOR == idOrder &&
               data.FK_ORDER == idProduct)

// Correct:
.Where(data => data.FK_ORDER == idOrder &&
               data.FK_INVENTORYCOLOR == idProduct)
```

---

### C-2. `BuyInventoryRepository.MarkBuysUsed()` — never executes; wrong SQL

**File:** `TailorProTrack.infraestructure/Repositories/BuyInventoryRepository.cs:123-126`  
**Risk:** No `BuyInventory` record is ever marked as USED via this path. The USED flag stays stale, breaking the guard against deleting purchases that were already consumed.

Two independent bugs:
1. `FromSqlRaw` returns `IQueryable`; it is never materialized, so EF Core never sends the SQL.
2. `WHERE USED = NULL` is always false in SQL — null comparisons require `IS NULL`.

```csharp
// Current (dead code):
_ctx.Set<BuyInventory>().FromSqlRaw("UPDATE BUY_INVENTORY SET USED = 1 WHERE USED = NULL");

// Correct:
_ctx.Database.ExecuteSqlRaw("UPDATE BUY_INVENTORY SET USED = 1 WHERE USED IS NULL");
```

---

### C-3. `SalesRepository.Save()` — ITBIS applied when it should not be (`||` instead of `&&`)

**File:** `TailorProTrack.infraestructure/Repositories/SalesRepository.cs:44`  
**Risk:** A sale with `ITBIS = 0` (explicitly no tax) triggers an 18% tax calculation, inflating `TOTAL_AMOUNT`. The invoice total is wrong and all downstream payment calculations inherit the inflated value.

```csharp
// Current (wrong — || means null check and value check are independent):
if (entity.ITBIS != null || entity.ITBIS >= 0)

// Correct:
if (entity.ITBIS != null && entity.ITBIS > 0)
```

---

### C-4. No authentication on any financial controller

**File:** `TailorProTrack.Api/Program.cs` and all financial controllers  
**Risk:** Any unauthenticated HTTP client can create/delete payments, cancel orders, record purchases, issue credit notes, or view all financial data.

`Program.cs` calls `app.UseAuthorization()` but never registers an authentication scheme (`AddAuthentication` / `AddJwtBearer`). None of the following controllers carry `[Authorize]`:
- `PaymentController`
- `PaymentExpensesController`
- `OrderController`
- `SaleController`
- `BuyInventoryController`
- `NoteCreditController`

**Fix:** Register a JWT Bearer scheme in `Program.cs` and add `[Authorize]` at class level on every financial controller.

---

## HIGH

### H-1. `SalesRepository.Update()` — wrong FK passed and total never saved to the persisted entity

**File:** `TailorProTrack.infraestructure/Repositories/SalesRepository.cs:71-85`  
**Risk:** Every `UpdateSale` call persists a wrong (likely zero) total amount.

Three compounding bugs:
1. `GetAmountByIdPreOrder` receives `entity.ID` (Sales ID) instead of `entity.FK_PREORDER`.
2. The result is stored in `entity.TOTAL_AMOUNT`, not `sale.TOTAL_AMOUNT`.
3. `_context.Update(sale)` is called, so the stale `sale.TOTAL_AMOUNT` is what actually persists.

```csharp
// Current:
entity.TOTAL_AMOUNT = _preOrderProductsRepository.GetAmountByIdPreOrder(entity.ID); // wrong FK
if (entity.ITBIS != null) entity.TOTAL_AMOUNT += (decimal)entity.ITBIS;             // modifies entity
this._context.Update(sale);                                                           // sale.TOTAL_AMOUNT unchanged

// Correct:
sale.TOTAL_AMOUNT = _preOrderProductsRepository.GetAmountByIdPreOrder(entity.FK_PREORDER);
if (sale.ITBIS != null && sale.ITBIS > 0) sale.TOTAL_AMOUNT += (decimal)sale.ITBIS;
this._context.Update(sale);
```

---

### H-2. `PaymentService.Remove()` — returns `Success = true` when deletion is blocked

**File:** `TailorProTrack.Application/Service/PaymentService.cs:253-258`  
**Risk:** Frontend receives HTTP 200 OK for a blocked deletion. The client has no way to detect that the payment was NOT deleted and shows a stale UI.

```csharp
// Current:
result.Message = "No se puede eliminar el ultimo pago de una orden";
result.Success = true;   // BUG

// Correct:
result.Success = false;
```

---

### H-3. `OrderRepository.Remove()` — no transaction; partial inventory restoration on failure

**File:** `TailorProTrack.infraestructure/Repositories/OrderRepository.cs:55-75`  
**Risk:** If the restoration loop fails mid-iteration, items 1…N-1 have their inventory restored and committed while items N…end are not. The order record still exists. Result: permanently corrupted stock levels.

Each `_inventoryColorRepository.Update()` and `_inventoryRepository.UpdateQuantityInventory()` call `SaveChanges()` internally. There is no wrapping transaction.

**Fix:** Wrap the entire `Remove` method body in a `BeginTransaction / Commit / Rollback` block.

---

### H-4. `BuyInventoryRepository.Remove()` — no transaction, bank balances not adjusted, no negative-stock guard

**File:** `TailorProTrack.infraestructure/Repositories/BuyInventoryRepository.cs:88-101`  
**Risk:** Deleting a purchase can leave bank accounts with incorrect balances and `InventoryColor.QUANTITY` can go negative, making future order stock validation pass when it should fail.

Three issues:
1. No transaction — `Expenses` are deleted before `RemoveInventoryByBuy` runs; a partial failure leaves orphan expense deletions.
2. `PaymentExpenses` recorded against the deleted `Expenses` records are not handled — `CREDIT_AMOUNT` on the bank account is not recalculated.
3. `RemoveInventoryByBuy` subtracts quantity with no non-negative guard (see M-3).

---

### H-5. `ExpensesRepository` GET methods — read-side mutation (`ConfirmExpenses` called on GET)

**File:** `TailorProTrack.infraestructure/Repositories/ExpensesRepository.cs:71-88, 153-170, 172-189`  
**Risk:** Any GET call to `GetExpensesPending()`, `GetBuysPending()`, or `GetOnlyExpensesPending()` permanently alters expense state. Retrying a read, running a health check, or loading a report page confirms expenses that may not be fully paid.

```csharp
// Inside GetExpensesPending():
else { this.ConfirmExpenses(expense.ID); }   // WRITE on GET path
```

This is the same pattern that was already fixed in `PreOrderRepository.GetAccountsReceivable()`.

**Fix:** Remove `ConfirmExpenses` from all three `Get*` methods. Trigger confirmation only on write paths (after a payment is recorded).

---

## MEDIUM

### M-1. `NoteCreditPaymentRepository.ExtractAmount()` — no `Update()` or `SaveChanges()`

**File:** `TailorProTrack.infraestructure/Repositories/NoteCreditRepository.cs:34-39`  
**Risk:** If the `NoteCredit` entity was loaded as `AsNoTracking()`, the balance reduction is silently lost and the credit note retains an inflated balance.

```csharp
// Current:
note.AMOUNT -= amount;
// No Update(note) or SaveChanges() here

// Fix:
note.AMOUNT -= amount;
_context.Set<NoteCredit>().Update(note);
_context.SaveChanges();
```

---

### M-2. `SaleService.Add()` — PreOrder ITBIS flag committed before Sale is saved

**File:** `TailorProTrack.Application/Service/SaleService.cs:29-44`  
**Risk:** If the Sale save throws, the PreOrder.ITBIS flag is already flipped in the database, permanently altering all subsequent payment calculations for that order.

```csharp
_preOrderRepository.Update(preOrder);   // commits — no rollback if next line fails
return base.Add(dtoAdd);
```

**Fix:** Wrap both operations in a single transaction, or defer the ITBIS flag update until after the Sale is confirmed saved.

---

### M-3. `InventoryRepository.RemoveInventoryByBuy()` — no non-negative stock guard

**File:** `TailorProTrack.infraestructure/Repositories/InventoryRepository.cs:164-179`  
**Risk:** `InventoryColor.QUANTITY` can go negative. Future order validation (`QUANTITY >= requested`) then behaves unpredictably.

```csharp
// Current:
invColor.QUANTITY -= item.QUANTITY;   // no guard

// Fix:
if (invColor.QUANTITY < item.QUANTITY)
    throw new InvalidOperationException("Cannot reduce stock below zero.");
invColor.QUANTITY -= item.QUANTITY;
```

---

### M-4. `BankAccountRepository.AddBalance` / `SubstractBalance` — bypass DEBIT/CREDIT reconciliation

**File:** `TailorProTrack.infraestructure/Repositories/BankAccountRepository.cs:39-58`  
**Risk:** If these methods are ever called, `BALANCE` diverges from `DEBIT_AMOUNT - CREDIT_AMOUNT`. The next `RecalculateBankAccount` call (used by payment repositories) silently overrides the manual change, making the correction appear to vanish.

All other balance mutations derive `BALANCE = DEBIT_AMOUNT - CREDIT_AMOUNT`. These two methods modify `BALANCE` directly without touching the component fields.

**Fix:** Either update `DEBIT_AMOUNT` alongside `BALANCE` in these methods, or deprecate them and route all balance changes through `RecalculateBankAccount`.

---

### M-5. `USER_CREATED` hardcoded to `1` in multiple repositories

**Files:**
- `PaymentRepository.cs:37, 207`
- `BuyInventoryRepository.cs:42, 51`
- `InventoryRepository.cs:66, 77, 92`

**Risk:** Audit trail is incorrect — all financial records appear created by user ID 1 regardless of who performed the action. This is a compliance issue for any financial audit.

**Fix:** Remove the hardcoded assignments. Propagate the authenticated user ID from the HTTP context through to the repository layer.

---

## LOW

### L-1. `PreOrderRepository.GetPreOrdersByRecentDate()` — returns oldest, not newest

**File:** `TailorProTrack.infraestructure/Repositories/PreOrderRepository.cs:39-42`

```csharp
// Current (ascending = oldest first):
.OrderBy(x => x.CREATED_AT).Take(10)

// Fix:
.OrderByDescending(x => x.CREATED_AT).Take(10)
```

---

### L-2. `OrderProductRepository.Remove()` — does not call `SaveChanges()`

**File:** `TailorProTrack.infraestructure/Repositories/OrderProductRepository.cs:41-45`  
Only marks the entity for removal without committing. The delete fires only if the caller happens to call `SaveChanges()` afterwards. Silent no-op for callers that do not.

---

## INFO

### I-1. `OrderProductRepository.SaveMany()` — empty foreach loop over buy details

**File:** `TailorProTrack.infraestructure/Repositories/OrderProductRepository.cs:60-68`  
The loop iterates `buyDetails` with an empty body. No immediate financial risk but signals incomplete business logic.

---

### I-2. `ValidationOrderExtention` — user FK validation is commented out

**File:** `TailorProTrack.Application/Extentions/ValidationOrderExtention.cs:24-27`  
Orders can be created with any arbitrary `FK_USER`. Referential integrity is enforced only at the database level if a FK constraint exists.

---

## Recommended fix order

| Priority | Issue | Effort |
|---|---|---|
| 1 | C-1: Fix swapped FK args in `GetByForeignKeys` | Trivial |
| 2 | C-3: Fix ITBIS `||` → `&&` condition | Trivial |
| 3 | H-2: Fix `PaymentService.Remove()` returning `Success = true` | Trivial |
| 4 | L-1: Fix `OrderByDescending` on recent preorders | Trivial |
| 5 | C-2: Fix `MarkBuysUsed()` with `ExecuteSqlRaw` + `IS NULL` | Low |
| 6 | H-5: Remove `ConfirmExpenses` from GET methods | Low |
| 7 | M-1: Add `Update()` + `SaveChanges()` in `ExtractAmount` | Low |
| 8 | L-2: Add `SaveChanges()` to `OrderProductRepository.Remove()` | Low |
| 9 | H-1: Fix `SalesRepository.Update()` — wrong FK and wrong target entity | Medium |
| 10 | M-2: Wrap `SaleService.Add()` ITBIS flag update in transaction | Medium |
| 11 | M-4: Fix `AddBalance`/`SubstractBalance` DEBIT/CREDIT bypass | Medium |
| 12 | H-3: Wrap `OrderRepository.Remove()` in a transaction | Medium |
| 13 | H-4: Wrap `BuyInventoryRepository.Remove()` in a transaction + recalculate bank | High |
| 14 | M-3: Add non-negative stock guard in `RemoveInventoryByBuy` | Low |
| 15 | M-5: Remove hardcoded `USER_CREATED = 1` | Medium |
| 16 | C-4: Add authentication and `[Authorize]` to financial controllers | High |
