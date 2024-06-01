

namespace TailorProTrack.Application.Dtos.ProductColor
{
    public class ProductColorDtoUpdate
    {
        public int Id {  get; set; }
        public int FkProduct {  get; set; }

        public int FkColor {  get; set; }
    }
}
