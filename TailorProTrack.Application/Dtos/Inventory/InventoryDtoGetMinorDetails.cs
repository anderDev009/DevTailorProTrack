

using TailorProTrack.Application.Dtos.InventoryColor;

namespace TailorProTrack.Application.Dtos.Inventory
{
    public class InventoryDtoGetMinorDetails : QuantityDto
    {
        public int idSize {  get; set; } 
        public string size {  get; set; }

        public List<InventoryColorDtoGetWithDetail> inventoryColor { get; set; } 
    }
}
