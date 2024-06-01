

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.OrderProduct;

namespace TailorProTrack.Application.Contracts
{
    public interface IOrderProductService : IBaseService<OrderProductDtoAdd
                                                        ,OrderProductDtoRemove
                                                        ,OrderProductDtoUpdate>
    {
        ServiceResult AddMany(List<OrderProductDtoAdd> products, int idOrder);
        ServiceResult GetQuantityByOrderId(int idOrder);
    }
}
