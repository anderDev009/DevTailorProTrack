using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.InventoryColor;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Contracts
{
    public interface IInventoryColorService : IBaseService<InventoryColorDtoAdd,
                                                           InventoryColorDtoRemove,
                                                           InventoryColorDtoUpdate>
    {
        ServiceResult AddAndUpdateInventory(InventoryColorDtoAdd dtoAdd);
        ServiceResult GetQuantityTotalById(int id);

        ServiceResult GetByIdInventory(int id);

        int GetIdInventory(int inventoryColorId);

        InventoryColorDtoGetWithId SearchAvailabilityToAddOrder(int sizeId,  int orderId);
    }
}
