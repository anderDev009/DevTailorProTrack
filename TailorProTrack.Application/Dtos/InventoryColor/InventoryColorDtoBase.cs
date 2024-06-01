namespace TailorProTrack.Application.Dtos.InventoryColor
{
    public class InventoryColorDtoBase : BaseDto
    {
        public int fk_color_primary { get; set; }
        public int fk_color_secondary { get; set; }
        public int quantity { get; set; }
        public int fk_inventory { get; set; }

    }
}
