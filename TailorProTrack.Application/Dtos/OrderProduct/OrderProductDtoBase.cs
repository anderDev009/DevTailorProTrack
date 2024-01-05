


namespace TailorProTrack.Application.Dtos.OrderProduct
{
    public class OrderProductDtoBase 
    {
        public int FkOrder {  get; set; }
        public int FkInventoryColor {  get; set; }
        public int Quantity { get; set; }
    }
}
