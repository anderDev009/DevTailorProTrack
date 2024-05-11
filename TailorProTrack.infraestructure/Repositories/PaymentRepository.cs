
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
            entity.USER_CREATED = 1;
            //logica para sumarle el monto a la cuenta
            if(entity.FK_BANK_ACCOUNT != null)
            {
                BankAccount account = _context.Set<BankAccount>().Find((int)entity.FK_BANK_ACCOUNT);
                if(account == null) 
                {
                    throw new Exception("Cuenta de banco invalida.");
                }
                //sumandole el monto a la cuenta
                account.BALANCE += entity.AMOUNT;
                //actualizando el monto
                _context.Set<BankAccount>().Update(account);
            }
           
            this._context.Add(entity); 
            this._context.SaveChanges();
            return entity.ID;
        }



        public override void Remove(Payment entity)
        {
            //logica para restarle el saldo en caso de que un pago sea cancelado
            if(entity.FK_BANK_ACCOUNT != 0)
            {
                BankAccount account = _context.Set<BankAccount>().Find(entity.FK_BANK_ACCOUNT);
                account.BALANCE += entity.AMOUNT;
                _context.Set<BankAccount>().Update(account);
                _context.SaveChanges();
            } 
            base.Remove(entity);
        }
    }
}
