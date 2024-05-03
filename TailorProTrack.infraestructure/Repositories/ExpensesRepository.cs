

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class ExpensesRepository : BaseRepository<Expenses>, IExpensesRepository    
    {
        private readonly TailorProTrackContext _context;
        public ExpensesRepository(TailorProTrackContext ctx) : base(ctx)
        {
            _context = ctx;
        }

        public override int Save(Expenses entity)
        {
            entity.CREATED_AT = DateTime.Now;
            entity.USER_CREATED = 1;
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.ID;

        }

    

  
    }
}
