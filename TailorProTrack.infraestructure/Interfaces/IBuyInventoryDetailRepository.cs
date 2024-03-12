

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IBuyInventoryDetailRepository : IBaseRepository<BuyInventoryDetail>
    {
        bool SaveMany(List<BuyInventoryDetail> list);
    }
}
