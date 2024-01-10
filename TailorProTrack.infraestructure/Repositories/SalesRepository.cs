

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class SalesRepository : BaseRepository<Sales>, ISalesRepository
    {
        private readonly TailorProTrackContext _context; 
        
        public SalesRepository(TailorProTrackContext context) : base(context)
        {
            _context = context;
        }


        public override int Save(Sales entity)
        {
            entity.CREATED_AT = DateTime.Now;

            this._context.Add(entity);
            this._context.SaveChanges();

            return entity.ID;
        }

        public override void Update(Sales entity)
        {
            Sales sale = this.GetEntity(entity.ID);

            sale.COD_ISC = entity.COD_ISC;
            sale.FK_ORDER = entity.FK_ORDER;
            sale.USER_MOD = entity.USER_MOD;
            sale.MODIFIED_AT = DateTime.Now;

            this._context.Update(sale);
            this._context.SaveChanges();
        }

        public override void Remove(Sales entity)
        {
            Sales sale = this.GetEntity(entity.ID);

            sale.USER_MOD = entity.USER_MOD;
            sale.MODIFIED_AT = DateTime.Now;
            sale.REMOVED = true;
        }
    }
}
