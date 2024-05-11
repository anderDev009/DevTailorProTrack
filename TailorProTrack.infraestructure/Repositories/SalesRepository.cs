

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class SalesRepository : BaseRepository<Sales>, ISalesRepository
    {
        private readonly TailorProTrackContext _context;
        private readonly IPreOrderProductsRepository _preOrderProductsRepository;
        public SalesRepository(TailorProTrackContext context, IPreOrderProductsRepository preOrderProductsRepository) : base(context)
        {
            _context = context;
            _preOrderProductsRepository = preOrderProductsRepository;
        }


        public override int Save(Sales entity)
        {
            entity.CREATED_AT = DateTime.Now;
            entity.TOTAL_AMOUNT = _preOrderProductsRepository.GetAmountByIdPreOrder(entity.ID);
            if(entity.ITBIS != null)
            {
                entity.TOTAL_AMOUNT += (decimal)entity.ITBIS;
            }
            this._context.Add(entity);
            this._context.SaveChanges();

            return entity.ID;
        }


        public override void Update(Sales entity)
        {
            Sales sale = this.GetEntity(entity.ID);

            sale.COD_ISC = entity.COD_ISC;
            sale.FK_PREORDER = entity.FK_PREORDER;
            sale.USER_MOD = entity.USER_MOD;
            sale.MODIFIED_AT = DateTime.Now;
            entity.TOTAL_AMOUNT = _preOrderProductsRepository.GetAmountByIdPreOrder(entity.ID);
            if (entity.ITBIS != null)
            {
                entity.TOTAL_AMOUNT += (decimal)entity.ITBIS;
            }
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

        public void ConfirmSale(int idSales)
        {
            Sales sale = _context.Set<Sales>().Find(idSales);
            sale.INVOICED = true;
            _context.Set<Sales>().Update(sale);
            _context.SaveChanges();
        }
    }
}
