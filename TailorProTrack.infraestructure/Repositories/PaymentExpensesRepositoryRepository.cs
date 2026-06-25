using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class PaymentExpensesRepositoryRepository : BaseRepository<PaymentExpenses>, IPaymentExpensesRepository
    {
        private readonly TailorProTrackContext _ctx;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IExpensesRepository _expensesRepository;

        public PaymentExpensesRepositoryRepository(TailorProTrackContext ctx,
            IBankAccountRepository bankAccountRepository,
            IExpensesRepository expensesRepository) : base(ctx)
        {
            _ctx = ctx;
            _bankAccountRepository = bankAccountRepository;
            _expensesRepository = expensesRepository;
        }

        public override int Save(PaymentExpenses entity)
        {
            var ownsTransaction = _ctx.Database.CurrentTransaction == null;
            using var transaction = ownsTransaction ? _ctx.Database.BeginTransaction() : null;

            try
            {
                if (entity.AMOUNT <= 0)
                {
                    throw new Exception("El monto del pago debe ser mayor a cero");
                }

                var expense = _ctx.Set<Expenses>().Find(entity.FK_EXPENSE);
                if (expense == null || expense.REMOVED)
                {
                    throw new Exception("El gasto no existe");
                }

                var amountPending = _expensesRepository.GetAmountPending(entity.FK_EXPENSE);
                if (amountPending <= 0)
                {
                    throw new Exception("Este gasto no tiene monto pendiente por pagar");
                }

                if (entity.AMOUNT > amountPending)
                {
                    throw new Exception($"El pago no puede ser mayor al monto pendiente ({amountPending})");
                }

                if (entity.FK_BANK_ACCOUNT != null && entity.FK_BANK_ACCOUNT != 0)
                {
                    BankAccount account = _bankAccountRepository.GetEntity((int)entity.FK_BANK_ACCOUNT);
                    //confirmamos que cuente con el balance suficiente y actualizamos
                    if (account.BALANCE < entity.AMOUNT)
                    {
                        throw new Exception("No cuenta con el balance suficiente");
                    }

                    account.CREDIT_AMOUNT += entity.AMOUNT;
                    account.BALANCE = account.DEBIT_AMOUNT - account.CREDIT_AMOUNT;
					_bankAccountRepository.Update(account);
                }

                int id = base.Save(entity);
                RecalculateExpenseCompletion(entity.FK_EXPENSE);

                transaction?.Commit();
                return id;
            }
            catch
            {
                transaction?.Rollback();
                throw;
            }
        }

        public override void Remove(PaymentExpenses entity)
        {
	        var ownsTransaction = _ctx.Database.CurrentTransaction == null;
	        using var transaction = ownsTransaction ? _ctx.Database.BeginTransaction() : null;

	        try
	        {
		        var entityToRemove = this.GetEntity(entity.ID);
		        if (entityToRemove == null)
		        {
			        throw new Exception("Pago no encontrado.");
		        }

		        var idBankAccount = entityToRemove.FK_BANK_ACCOUNT;
		        var idExpense = entityToRemove.FK_EXPENSE;
		        _ctx.Remove(entityToRemove);
				_ctx.SaveChanges();
				if (idBankAccount != null && idBankAccount > 0)
				{
					RecalculateBankAccount((int)idBankAccount);
				}

				RecalculateExpenseCompletion(idExpense);

				transaction?.Commit();
	        }
	        catch
	        {
		        transaction?.Rollback();
		        throw;
	        }
		}

        public override void Update(PaymentExpenses entity)
        {
	        var ownsTransaction = _ctx.Database.CurrentTransaction == null;
	        using var transaction = ownsTransaction ? _ctx.Database.BeginTransaction() : null;

	        try
	        {
		        if (entity.AMOUNT <= 0)
		        {
			        throw new Exception("El monto del pago debe ser mayor a cero");
		        }

		        var paymentToUpdate = this.GetEntity(entity.ID);
		        if (paymentToUpdate == null || paymentToUpdate.REMOVED)
		        {
			        throw new Exception("Pago no encontrado.");
		        }

		        var expense = _ctx.Set<Expenses>().Find(entity.FK_EXPENSE);
		        if (expense == null || expense.REMOVED)
		        {
			        throw new Exception("El gasto no existe");
		        }

		        var previousExpenseId = paymentToUpdate.FK_EXPENSE;
		        var previousBankAccountId = paymentToUpdate.FK_BANK_ACCOUNT;
		        var amountPending = _expensesRepository.GetAmountPending(entity.FK_EXPENSE);
		        var amountAvailable = amountPending + (previousExpenseId == entity.FK_EXPENSE ? paymentToUpdate.AMOUNT : 0);

		        if (amountAvailable <= 0)
		        {
			        throw new Exception("Este gasto no tiene monto pendiente por pagar");
		        }

		        if (entity.AMOUNT > amountAvailable)
		        {
			        throw new Exception($"El pago no puede ser mayor al monto pendiente ({amountAvailable})");
		        }

		        if (entity.FK_BANK_ACCOUNT != null && entity.FK_BANK_ACCOUNT != 0)
		        {
			        var account = _ctx.Set<BankAccount>().Find(entity.FK_BANK_ACCOUNT);
			        if (account == null)
			        {
				        throw new Exception("Cuenta inexistente");
			        }

			        var availableBalance = account.BALANCE;
			        if (previousBankAccountId == entity.FK_BANK_ACCOUNT)
			        {
				        availableBalance += paymentToUpdate.AMOUNT;
			        }

			        if (availableBalance < entity.AMOUNT)
			        {
				        throw new Exception("No cuenta con el balance suficiente");
			        }
		        }

		        paymentToUpdate.FK_EXPENSE = entity.FK_EXPENSE;
		        paymentToUpdate.FK_PAYMENT_TYPE = entity.FK_PAYMENT_TYPE;
		        paymentToUpdate.FK_BANK_ACCOUNT = entity.FK_BANK_ACCOUNT;
		        paymentToUpdate.AMOUNT = entity.AMOUNT;
		        paymentToUpdate.MODIFIED_AT = DateTime.Now;
		        paymentToUpdate.USER_MOD = entity.USER_MOD;
		        _ctx.Set<PaymentExpenses>().Update(paymentToUpdate);
		        _ctx.SaveChanges();

		        RecalculateBankAccounts(previousBankAccountId, entity.FK_BANK_ACCOUNT);
		        RecalculateExpenseCompletion(previousExpenseId);
		        if (previousExpenseId != entity.FK_EXPENSE)
		        {
			        RecalculateExpenseCompletion(entity.FK_EXPENSE);
		        }

		        transaction?.Commit();
	        }
	        catch
	        {
		        transaction?.Rollback();
		        throw;
	        }
        }

        public decimal GetCreditAmountTotal(int idAccount)
        {
	        return _ctx.Set<PaymentExpenses>().Where(x => x.FK_BANK_ACCOUNT == idAccount && !x.REMOVED).Sum(x => x.AMOUNT);
        }

        public decimal GetCreditThisMonth(int idAccount)
        {
	        var now = DateTime.Now;
	        var firstDayMonth = new DateTime(now.Year, now.Month, 1);
	        return _ctx.Set<PaymentExpenses>().Where(x => x.CREATED_AT >= firstDayMonth && x.FK_BANK_ACCOUNT == idAccount && !x.REMOVED).Sum(x => x.AMOUNT);
		}

		private void RecalculateBankAccounts(int? previousBankAccountId, int? currentBankAccountId)
		{
			if (previousBankAccountId != null && previousBankAccountId > 0)
			{
				RecalculateBankAccount((int)previousBankAccountId);
			}

			if (currentBankAccountId != null && currentBankAccountId > 0 && currentBankAccountId != previousBankAccountId)
			{
				RecalculateBankAccount((int)currentBankAccountId);
			}
		}

		private void RecalculateBankAccount(int idBankAccount)
		{
			BankAccount account = _ctx.Set<BankAccount>().Find(idBankAccount);
			if (account == null)
			{
				throw new Exception("Cuenta inexistente");
			}

			account.CREDIT_AMOUNT = this.GetCreditAmountTotal(idBankAccount);
			account.BALANCE = account.DEBIT_AMOUNT - account.CREDIT_AMOUNT;
			_ctx.Set<BankAccount>().Update(account);
			_ctx.SaveChanges();
		}

		private void RecalculateExpenseCompletion(int idExpense)
		{
			Expenses expense = _ctx.Set<Expenses>().Find(idExpense);
			if (expense == null)
			{
				throw new Exception("El gasto no existe");
			}

			expense.COMPLETED = _expensesRepository.GetAmountPending(idExpense) <= 0;
			_ctx.Set<Expenses>().Update(expense);
			_ctx.SaveChanges();
		}
	}
}
