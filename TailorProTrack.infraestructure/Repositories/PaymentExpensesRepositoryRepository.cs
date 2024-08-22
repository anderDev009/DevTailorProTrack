
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

        public PaymentExpensesRepositoryRepository(TailorProTrackContext ctx,
            IBankAccountRepository bankAccountRepository) : base(ctx)
        {
            _ctx = ctx;
            _bankAccountRepository = bankAccountRepository;
        }

        public override int Save(PaymentExpenses entity)
        {
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
            return base.Save(entity);
        }

        public override void Remove(PaymentExpenses entity)
        {
	        var entityToRemove = this.GetEntity(entity.ID);
	        _ctx.Remove(entityToRemove);
			_ctx.SaveChanges();
			if (entity.FK_BANK_ACCOUNT != null && entity.FK_BANK_ACCOUNT > 0)
			{
				BankAccount account = _ctx.Set<BankAccount>().Find(entity.FK_BANK_ACCOUNT);
				account.CREDIT_AMOUNT = this.GetCreditAmountTotal((int)entity.FK_BANK_ACCOUNT);
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
