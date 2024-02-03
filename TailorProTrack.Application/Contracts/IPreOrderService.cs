

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.PreOrder;

namespace TailorProTrack.Application.Contracts
{
    public interface IPreOrderService : IBaseService<PreOrderDtoAdd, PreOrderDtoRemove, PreOrderDtoUpdate>
    {
    }
}
