

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IBuyInventoryRepository : IBaseRepository<BuyInventory>
    {
        bool AddBuyInventory(BuyInventory buyInventory, List<BuyInventoryDetail> detail );
        bool CheckUsed(int id);
    }
}
