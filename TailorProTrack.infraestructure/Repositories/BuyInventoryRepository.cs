
using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class BuyInventoryRepository : BaseRepository<BuyInventory>, IBuyInventoryRepository
    {
        private readonly TailorProTrackContext _ctx;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IExpensesRepository _expensesRepository;
        public BuyInventoryRepository(TailorProTrackContext ctx, IInventoryRepository inventoryRepository,IExpensesRepository expensesRepository) : base(ctx)
        {
            _ctx = ctx;
            _inventoryRepository = inventoryRepository;
            _expensesRepository = expensesRepository;
		}

        public override BuyInventory GetEntity(int id)
        {
            return _ctx.Set<BuyInventory>()
                .Include(x => x.Details)
                .ThenInclude(x => x.Product)
                .Include(x => x.Details)
                .ThenInclude(x => x.ColorPrimary)
                .Include(x => x.Details)
                .ThenInclude(x => x.ColorSecondary)
                .Include(x => x.Details)
                .ThenInclude(x => x.Size)
                .FirstOrDefault(x => x.ID == id);
        }
        public bool AddBuyInventory(BuyInventory buyInventory, List<BuyInventoryDetail> detail)
        {
            buyInventory.USER_CREATED = 1;
            buyInventory.CREATED_AT = DateTime.Now;
            buyInventory.DATE_MADE = DateTime.Now;
            _ctx.Set<BuyInventory>().Add(buyInventory);
            _ctx.SaveChanges();
           

            foreach(var item in detail)
            {
                item.FK_BUY_INVENTORY = buyInventory.ID;
                item.USER_CREATED = 1;
                item.CREATED_AT =DateTime.Now;
			}
            
            _ctx.Set<BuyInventoryDetail>().AddRange(detail);
            int success = _ctx.SaveChanges();
            _inventoryRepository.AddInventoryByBuy(detail);
            //creando gasto
            _expensesRepository.Save(new    ()
            {
	            AMOUNT = buyInventory.TOTAL_SALE,
	            COMPLETED = false,
	            CREATED_AT = DateTime.UtcNow,
	            DESCR = "Compra de inventario",
	            NAME = $"Compra de inventario a {buyInventory.COMPANY}",
	            DOCUMENT_NUMBER = $"{buyInventory.NCF}",
	            USER_CREATED = 1,
	            VOUCHER = $"{buyInventory.NCF}",
                FK_BUY = buyInventory.ID

            });
			if (success == 0)
            {
                return false;
            }
            return true; 
        }

        public override void Remove(BuyInventory entity)
        {
            //eliminando los gastos
            var expenses = _ctx.Set<Expenses>().Where(x => x.FK_BUY == entity.ID).ToList();
            _ctx.Set<Expenses>().RemoveRange(expenses);

            //eliminando los detalles
            var details = _ctx.Set<BuyInventoryDetail>().Where(x => x.FK_BUY_INVENTORY == entity.ID).ToList();
            //eliminando el inventario
            _inventoryRepository.RemoveInventoryByBuy(details);
            //eliminando la compra
            _ctx.Set<BuyInventory>().Remove(entity);
            _ctx.SaveChanges();
        }

        public bool CheckUsed(int id)
        {
            var buy = _ctx.Set<BuyInventory>().Find(id);
            if (buy == null)
            {
                return false;
            }
            //marcando la compra como utilizada
            buy.USED = true;
            _ctx.Set<BuyInventory>().Update(buy);
            _ctx.SaveChanges();
            return true;
        }

        public List<BuyInventory> GetBuysByDate(DateTime startDate, DateTime endDate)
        {
            var buys = _ctx.Set<BuyInventory>().Include(x => x.Details).Where(x => x.DATE_MADE >= startDate && x.DATE_MADE <= endDate).ToList();
            return buys;
        }

        public void MarkBuysUsed()
        {
            _ctx.Set<BuyInventory>().FromSqlRaw("UPDATE BUY_INVENTORY SET USED = 1 WHERE USED = NULL");
        }
    }
}
