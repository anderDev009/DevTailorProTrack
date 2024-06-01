
using TailorProTrack.Application.Dtos.OrderProduct;

namespace TailorProTrack.Application.Dtos.Order
{
    public class OrderDtoAdd : OrderDtoBase
    {
        public string sendTo {  get; set; }
        public List<OrderProductDtoAdd>  products { get; set; }
    }
}
