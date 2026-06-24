
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.JavaScript;
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
            if (entity.AMOUNT == amountPending)
            {
                expense.COMPLETED = true;
                _ctx.Set<Expenses>().Update(expense);
                _ctx.SaveChanges();
            }

            return id;
        }

        public override void Remove(PaymentExpenses entity)
        {
	        var entityToRemove = this.GetEntity(entity.ID);
	        var idBankAccount = entityToRemove.FK_BANK_ACCOUNT;
	        _ctx.Remove(entityToRemove);
			_ctx.SaveChanges();
			if (idBankAccount != null && idBankAccount > 0)
			{
				BankAccount account = _ctx.Set<BankAccount>().Find(idBankAccount);
				account.CREDIT_AMOUNT = this.GetCreditAmountTotal((int)idBankAccount);
				account.BALANCE = account.DEBIT_AMOUNT - account.CREDIT_AMOUNT;
				_ctx.Set<BankAccount>().Update(account);
				_ctx.SaveChanges();
			}
		}

        public decimal GetCreditAmountTotal(int idAccount)
        {
	        return _ctx.Set<PaymentExpenses>().Where(x => x.FK_BANK_ACCOUNT == idAccount).Sum(x => x.AMOUNT);
        }

        public decimal GetCreditThisMonth(int idAccount)
        {
	        var now = DateTime.Now;
	        var firstDayMonth = new DateTime(now.Year, now.Month, 1);
	        return _ctx.Set<PaymentExpenses>().Where(x => x.CREATED_AT >= firstDayMonth && x.FK_BANK_ACCOUNT == idAccount).Sum(x => x.AMOUNT);
		}
	}
}
