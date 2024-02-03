
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
        public override void Update(BankAccount entity)
        {
            BankAccount bankAccount = this.GetEntity(entity.ID);

            bankAccount.FK_BANK = entity.FK_BANK;
            bankAccount.BANK_ACCOUNT = entity.BANK_ACCOUNT;
            bankAccount.USER_MOD = entity.USER_MOD;
            bankAccount.MODIFIED_AT = DateTime.Now;

            this._context.Update(bankAccount);
            this._context.SaveChanges();
        }

  
    }
}
