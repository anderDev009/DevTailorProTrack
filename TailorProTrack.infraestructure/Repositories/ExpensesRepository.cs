

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
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.ID;

        }

        public override void Update(Expenses entity)
        {
            Expenses expenses = this.GetEntity(entity.ID);

            expenses.NAME = entity.NAME;
            expenses.DESCR = entity.DESCR;
            expenses.VOUCHER = entity.VOUCHER;
            expenses.MODIFIED_AT = DateTime.Now;
            expenses.USER_MOD = entity.USER_MOD;

            this._context.Update(expenses);
            this._context.SaveChanges();

        }

        public override void Remove(Expenses entity)
        {
            Expenses expenses = this.GetEntity(entity.ID);

            expenses.REMOVED = entity.REMOVED;
            expenses.MODIFIED_AT = DateTime.Now;
            expenses.USER_MOD = entity.USER_MOD;

            this._context.Update(expenses);
            this._context.SaveChanges();
        }
    }
}
