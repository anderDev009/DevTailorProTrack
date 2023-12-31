
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Order;

namespace TailorProTrack.Application.Contracts
{
    public interface IOrderService : IBaseService<OrderDtoAdd,OrderDtoRemove,OrderDtoUpdate>
    {
    }
}
