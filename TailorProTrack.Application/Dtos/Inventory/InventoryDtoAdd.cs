using TailorProTrack.Application.Dtos.InventoryColor;

namespace TailorProTrack.Application.Dtos.Inventory
{
    public class InventoryDtoAdd : InventoryBaseDto
    {
        
        public List<InventoryColorDtoAdd> inventoryColors {  get; set; }
    }
}
