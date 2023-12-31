
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly TailorProTrackContext _context;

        public OrderRepository(TailorProTrackContext context) : base(context)
        {
            this._context = context;
        }

        public override int Save(Order entity)
        {
            entity.CREATED_AT = DateTime.Now;

            this._context.Add(entity);
            this._context.SaveChanges();

            return entity.ID;
        }

        public override void Update(Order entity)
        {
            Order order = this.GetEntity(entity.ID);

            order.CHECKED = entity.CHECKED;
            order.AMOUNT = entity.AMOUNT;
            order.FK_CLIENT = entity.FK_CLIENT;
            order.FK_USER = entity.FK_USER;
            order.MODIFIED_AT = DateTime.Now;
            order.USER_MOD = entity.USER_MOD;

            this._context.Update(order);
            this._context.SaveChanges();
        }

        public override void Remove(Order entity)
        {
            Order order = this.GetEntity(entity.ID);

            order.REMOVED = true;
            this._context.Update(order);
            this._context.SaveChanges();
        }
    }
}
