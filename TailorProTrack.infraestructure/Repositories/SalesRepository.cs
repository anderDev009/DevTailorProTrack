

using Microsoft.EntityFrameworkCore;
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
        public override List<Sales> GetEntitiesPaginated(int page, int itemsPage)
        {
            return this._context.Set<Sales>().Where(data => !data.REMOVED).Skip((page - 1) * itemsPage).Include(x => x.PreOrder).ThenInclude(x => x.Client).Take(itemsPage).ToList();
        }
		public override void Remove(Sales entity)
		{
			var entityToRemove = GetEntity(entity.ID);
            //marcando el ITBIS como 0 de la entidad pedido 
            var preOrder = _context.Set<PreOrder>()
                .Where(x => x.ID == entityToRemove.FK_PREORDER)
                .First();
            preOrder.ITBIS = false;
            _context.Set<PreOrder>().Update(preOrder);
			_context.Set<Sales>().Remove(entityToRemove);
			_context.SaveChanges();
		}
		public override int Save(Sales entity)
        {
            if (IsConfirmed(entity.FK_PREORDER))
            {
                throw new Exception("Pedido ya facturado");
            }
            entity.CREATED_AT = DateTime.Now;
            entity.TOTAL_AMOUNT = _preOrderProductsRepository.GetAmountByIdPreOrder(entity.FK_PREORDER);
            if(entity.ITBIS != null || entity.ITBIS >= 0)
            {
                entity.ITBIS = (decimal)((double)entity.TOTAL_AMOUNT * 18)/100;
                entity.TOTAL_AMOUNT += (decimal)entity.ITBIS;
            }
            this._context.Add(entity);
            this._context.SaveChanges();

            return entity.ID;
        }
        public override Sales GetEntity(int id)
        {
            return _context.Set<Sales>()
                .Include(x => x.PreOrder)
                .ThenInclude(x => x.PreOrderProducts).ThenInclude(x => x.Product)
                .Include(x => x.PreOrder)
                .ThenInclude(x => x.PreOrderProducts).ThenInclude(x => x.Size)
                .Include(x => x.PreOrder)
                .ThenInclude(x => x.PreOrderProducts).ThenInclude(x => x.ColorPrimary)
                .Include(x => x.PreOrder)
                .ThenInclude(x => x.PreOrderProducts).ThenInclude(x => x.ColorSecondary)
                .Include(x => x.PreOrder)
                .ThenInclude(x => x.Client)
                .Where(x => x.ID == id).FirstOrDefault();
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


        public void ConfirmSale(int idSales)
        {
            Sales sale = _context.Set<Sales>().Find(idSales);
            sale.INVOICED = true;
            _context.Set<Sales>().Update(sale);
            _context.SaveChanges();
        }

        private bool IsConfirmed(int idPreOrder)
        {
            bool isConfirmed = _context.Set<Sales>().Any(x => x.INVOICED == true && x.FK_PREORDER == idPreOrder);
            return isConfirmed;
        }

        public List<Sales> GetSalesInvoicedByDate(DateTime startDate, DateTime endDate)
        {
            var sales = _context.Set<Sales>().Where(x => x.CREATED_AT >= startDate && x.CREATED_AT <= endDate)
                        .Include(x => x.PreOrder)
                        .ThenInclude(x => x.Client)
                        .ToList();
            return sales;
        }
    }
}
