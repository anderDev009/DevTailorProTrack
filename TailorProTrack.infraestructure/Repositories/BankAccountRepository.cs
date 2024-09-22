
using Microsoft.EntityFrameworkCore.Update;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class BankAccountRepository : BaseRepository<BankAccount>, IBankAccountRepository
    {
        private readonly TailorProTrackContext _context;
        public BankAccountRepository(TailorProTrackContext ctx) : base(ctx)
        {
            _context = ctx;
        }

        public override int Save(BankAccount entity)
        {
            entity.DEBIT_AMOUNT = entity.BALANCE;
            int id = base.Save(entity);

            //creacion de cuenta debito
            if(entity.BALANCE > 0)
            {
                AccountDebit account = new AccountDebit();
                account.FK_BANK_ACC = id;
                account.FK_PAYMENT = null;
                account.AMOUNT = entity.BALANCE;
                account.CREATED_AT = DateTime.UtcNow;
                account.MODIFIED_AT = DateTime.UtcNow;

                _context.Set<AccountDebit>().Add(account);
                _context.SaveChanges();
            }
         
            return id;
        }
        public void AddBalance(int IdAccount, decimal Balance)
        {
            BankAccount account = _context.Set<BankAccount>().Find(IdAccount);
            if (account == null)
            {
                throw new Exception("Cuenta inexistente");
            }
            account.BALANCE += Balance;
            _context.SaveChanges();
        }
        public void SubstractBalance(int IdAccount, decimal Balance)
        {
            BankAccount account = _context.Set<BankAccount>().Find(IdAccount);
            if (account == null)
            {
                throw new Exception("Cuenta inexistente");
            }
            account.BALANCE -= Balance;
            _context.SaveChanges();
        }

    }
}
