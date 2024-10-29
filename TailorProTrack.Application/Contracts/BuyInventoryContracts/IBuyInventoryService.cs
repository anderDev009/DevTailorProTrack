

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.BuyInventoryDtos;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Contracts.BuyInventoryContracts
{
    public interface IBuyInventoryService : IBaseService<BuyInventoryDtoAdd,BuyInventoryDtoRemove,BuyInventoryDtoUpdate>
    {
        ServiceResult GetBuysByDate(DateTime startDate, DateTime endDate);

    }
}
