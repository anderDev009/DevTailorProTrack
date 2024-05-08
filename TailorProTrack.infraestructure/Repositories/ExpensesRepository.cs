

using System.Linq;
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
            //logica para descontar de la tarjeta 
            //obteniendo la cuenta de banco de la entidad
            if(entity.FK_BANK_ACCOUNT != null)
            {
                BankAccount account = _context.Set<BankAccount>().Find((int)entity.FK_BANK_ACCOUNT);
                
                if(account == null)
                {
                    throw new Exception("Cuenta de banco no existente.");
                }
                //en caso de que la cuenta tenga mas o igual que el monto solicitado
                if(account.BALANCE >= entity.AMOUNT)
                {
                    account.BALANCE -= entity.AMOUNT;
                }
                //actualizando la entidad
                _context.Set<BankAccount>().Update(account);

            }
            this._context.Add(entity);
            this._context.SaveChanges();
            return entity.ID;

        }

    

  
    }
}
