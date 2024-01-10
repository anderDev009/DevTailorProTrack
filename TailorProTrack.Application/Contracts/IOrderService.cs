
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Order;

namespace TailorProTrack.Application.Contracts
{
    public interface IOrderService : IBaseService<OrderDtoAdd,OrderDtoRemove,OrderDtoUpdate>
    {
        ServiceResult UpdateAmount(int Id);
        ServiceResult GetOrderJobs();
        ServiceResult UpdateStatusOrder(OrderDtoUpdateStatus dtoUpdate);
        ServiceResult GetOrder(int Id);
        ServiceResult GetAmountTotalById(int Id);
    }
}
