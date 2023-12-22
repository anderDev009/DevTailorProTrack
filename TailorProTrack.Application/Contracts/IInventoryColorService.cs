using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.InventoryColor;

namespace TailorProTrack.Application.Contracts
{
    public interface IInventoryColorService : IBaseService<InventoryColorDtoAdd,
                                                           InventoryColorDtoRemove,
                                                           InventoryColorDtoUpdate>
    {
        ServiceResult AddAndUpdateInventory(InventoryColorDtoAdd dtoAdd);
        ServiceResult GetQuantityTotalById(int id);

        ServiceResult GetByIdInventory(int id);
    }
}
