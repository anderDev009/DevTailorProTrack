
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
        public BuyInventoryRepository(TailorProTrackContext ctx, IInventoryRepository inventoryRepository) : base(ctx)
        {
            _ctx = ctx;
            _inventoryRepository = inventoryRepository;

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
            if(success == 0)
            {
                return false;
            }
            return true; 
        }
    }
}
