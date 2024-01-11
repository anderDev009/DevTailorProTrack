
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
            order.DESCRIPTION_JOB = entity.DESCRIPTION_JOB;

            this._context.Update(order);
            this._context.SaveChanges();
        }

        public override void Remove(Order entity)
        {
            Order order = this.GetEntity(entity.ID);

            order.REMOVED = true;
            order.USER_MOD = entity.USER_MOD;
            order.MODIFIED_AT = DateTime.Now;
            this._context.Update(order);
            this._context.SaveChanges();
        }

        public void UpdateAmount(Order order)
        {
            Order orderToUpdate = this.GetEntity(order.ID);

            //actualizando el monto
            orderToUpdate.AMOUNT = order.AMOUNT;

            this._context.Update(orderToUpdate);
            this._context.SaveChanges();
        }

        public void UpdateStatusOrder(Order order)
        {
            Order orderToUpdate = this.GetEntity(order.ID);
            //validarlo luego
            if (orderToUpdate == null) throw new Exception("No existe");
            orderToUpdate.STATUS_ORDER = order.STATUS_ORDER;
            orderToUpdate.USER_MOD = order.USER_MOD;
            orderToUpdate.MODIFIED_AT = DateTime.Now;

            this._context.Update(orderToUpdate);
            this._context.SaveChanges();
        }
    }
}
