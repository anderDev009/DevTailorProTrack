

namespace TailorProTrack.Application.Dtos.InventoryColor
{
    public class InventoryColorDtoGet 
    {
        public int InventoryColorId { get; set; }
        public int FkInventory {  get; set; }
        public object colorPrimary {  get; set; }
        public object colorSecondary {  get; set; }
        
        public int quantity {  get; set; }
    }
}
