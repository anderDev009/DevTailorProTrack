
using TailorProTrack.Application.Dtos.Product;

namespace TailorProTrack.Application.Dtos.InventoryColor
{
    public class InventoryColorDtoGetWithId : InventoryColorDtoGet
    {


        public object? Product { get; set; }
        public object? Size { get; set; }
        public int? QuantityPreOrder { get; set; }
    }
}
