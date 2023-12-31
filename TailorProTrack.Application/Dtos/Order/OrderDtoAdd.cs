
using TailorProTrack.Application.Dtos.OrderProduct;

namespace TailorProTrack.Application.Dtos.Order
{
    public class OrderDtoAdd : OrderDtoBase
    {

        public List<OrderProductDtoAdd>  products { get; set; }
    }
}
