using TailorProTrack.Application.Dtos.Product;

namespace TailorProTrack.Application.Dtos.Inventory
{
    public class InventoryDtoGetWithDetails : ProductDtoGet
    {
        public int quantity {  get; set; }

        public object inventory { get; set; }

    }
}
