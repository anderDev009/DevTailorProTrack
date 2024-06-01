
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IInventoryRepository : IBaseRepository<Inventory>
    {
        bool AddInventoryByBuy(List<BuyInventoryDetail> detail);
        bool UpdateQuantityInventory(int id);
    }
}
