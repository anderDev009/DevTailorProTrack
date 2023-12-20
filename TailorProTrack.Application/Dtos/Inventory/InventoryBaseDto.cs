

namespace TailorProTrack.Application.Dtos.Inventory
{
    public class InventoryBaseDto : BaseDto
    {
        public int fk_product {  get; set; }
        public int fk_size { get; set; }
    }   
}
