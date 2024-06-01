

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.BuyInventoryDtos;

namespace TailorProTrack.Application.Contracts.BuyInventoryContracts
{
    public interface IBuyInventoryService : IBaseService<BuyInventoryDtoAdd,BuyInventoryDtoRemove,BuyInventoryDtoUpdate>
    {
    }
}
