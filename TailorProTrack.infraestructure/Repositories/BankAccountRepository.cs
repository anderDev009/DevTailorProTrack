
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
