
using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class OrderProductRepository : BaseRepository<OrderProduct>, IOrderProductRepository
    {
        private readonly TailorProTrackContext _context; 
        public OrderProductRepository(TailorProTrackContext context) : base(context) 
        {
            _context = context;
        }

   

        public override int Save(OrderProduct entity)
        {
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.FK_ORDER;
        }

        public override void Update(OrderProduct entity)
        {
            OrderProduct orderP = this.GetByForeignKeys(entity.FK_ORDER, entity.FK_INVENTORYCOLOR);
            orderP.QUANTITY = entity.QUANTITY;
            orderP.FK_ORDER = entity.FK_ORDER;
            orderP.FK_INVENTORYCOLOR = entity.FK_INVENTORYCOLOR;

            this._context.Update(orderP);
            this._context.SaveChanges();
        }

        public override void Remove(OrderProduct entity)
        {
            OrderProduct orderP = this.GetEntity(entity.ID);
            this._context.Remove(orderP);
        }

        public OrderProduct GetByForeignKeys(int idOrder, int idProduct)
        {
            return this._entities
                                  .Where(data => data.FK_INVENTORYCOLOR == idOrder &&
                                         data.FK_ORDER == idProduct)
                                  .Single();

        }

        public void SaveMany(List<OrderProduct> products)
        {
            foreach(var item in products)
            {
                item.CREATED_AT = DateTime.Now; 
                this._context.Add(item);
            }
            this._context.SaveChanges();
        }

        public void ValidateMany(List<OrderProduct> products)
        {
            
            foreach(var item in products)
            {
                
            }
        }

 
    }
}
