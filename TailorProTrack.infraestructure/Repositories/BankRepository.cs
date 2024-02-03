

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        private readonly TailorProTrackContext _context;
        public BankRepository(TailorProTrackContext ctx): base(ctx) 
        {
            _context = ctx;
        }

        public override int Save(Bank entity)
        {
            entity.CREATED_AT = DateTime.Now;
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.ID;
        }

        public override void Update(Bank entity)
        {
            Bank bank = this.GetEntity(entity.ID);

            bank.NAME = entity.NAME;
            bank.USER_MOD = entity.USER_MOD;
            bank.MODIFIED_AT = DateTime.Now;

            this._context.Update(bank);
            this._context.SaveChanges();
        }

     
    }
}
