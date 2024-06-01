

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class PaymentTypeRepository : BaseRepository<PaymentType>, IPaymentTypeRepository
    {
        private readonly TailorProTrackContext _context;

        public PaymentTypeRepository(TailorProTrackContext context) : base(context)
        {
            _context = context;
        }

        public override int Save(PaymentType entity)
        {
            entity.CREATED_AT = DateTime.Now;

            this._context.Add(entity);
            this._context.SaveChanges();

            return entity.ID;
        }

        public override void Remove(PaymentType entity)
        {
            PaymentType paymentToRemove = this.GetEntity(entity.ID);
            
            paymentToRemove.MODIFIED_AT = DateTime.Now;
            paymentToRemove.USER_MOD = entity.USER_MOD;
            paymentToRemove.REMOVED = true;

            this._context.Update(paymentToRemove);
            this._context.SaveChanges();
        }

        public override void Update(PaymentType entity)
        {
            PaymentType paymentToUpdate = this.GetEntity(entity.ID);

            paymentToUpdate.TYPE_PAYMENT = entity.TYPE_PAYMENT;
            paymentToUpdate.MODIFIED_AT = DateTime.Now;
            paymentToUpdate.USER_MOD = entity.USER_MOD;

            this._context.Update(paymentToUpdate);
            this._context.SaveChanges();
        }

    }
}
