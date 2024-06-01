

namespace TailorProTrack.Application.Dtos.Order
{
     class OrderDtoListProduct : OrderDtoListProductBase
    {
        public int FkOrder {  get; set; }
        public decimal Cost { get; set; }
    }
}
