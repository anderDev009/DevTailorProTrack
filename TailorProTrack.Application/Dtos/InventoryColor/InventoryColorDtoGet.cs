
using TailorProTrack.Application.Dtos.Size;

namespace TailorProTrack.Application.Dtos.InventoryColor
{
    public class InventoryColorDtoGet
    {
        public string product_name {  get; set; }
        public List<SizeDtoGet> sizes { get; set; }
    }
}
