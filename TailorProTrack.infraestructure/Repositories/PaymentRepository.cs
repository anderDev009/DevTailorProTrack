
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>,IPaymentRepository
    {
        private readonly TailorProTrackContext _context;

        public PaymentRepository(TailorProTrackContext context) : base(context)
        {
            _context = context;
        }

        public override int Save(Payment entity)
        {
            entity.CREATED_AT = DateTime.Now;
            this._context.Add(entity); 
            this._context.SaveChanges();
            return entity.ID;
        }

        public override void Update(Payment entity)
        {
            Payment payment = this.GetEntity(entity.ID);

            payment.AMOUNT = entity.AMOUNT;
            payment.FK_ORDER = entity.FK_ORDER;
            payment.FK_TYPE_PAYMENT = entity.FK_TYPE_PAYMENT;
            payment.MODIFIED_AT = DateTime.Now;
            payment.USER_MOD = entity.USER_MOD;
            payment.FK_BANK_ACCOUNT = entity.FK_BANK_ACCOUNT;

            this._context.Update(payment);
            this._context.SaveChanges();
        }

        public override void Remove(Payment entity)
        {
            Payment payment = this.GetEntity(entity.ID);

            payment.USER_MOD = entity.USER_MOD;
            payment.MODIFIED_AT = DateTime.Now;
            payment.REMOVED = true;

            this._context.Update(payment);
            this._context.SaveChanges();
        }
    }
}
