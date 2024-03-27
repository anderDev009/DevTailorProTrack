using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IInventoryColorRepository : IBaseRepository<InventoryColor>
    {
        InventoryColor SearchAvailabilityToAddOrder(int SizeId, int OrderId, int colorPrimary, int? colorSecondary);
       
    }
}
