using TailorProTrack.domain.Core;

;

namespace TailorProTrack.domain.Entities
{
    public class ProductColor : BaseEntity
    {
        public int FK_PRODUCT { get; set; }
        public int FK_COLOR {  get; set; }
        public Color? Color { get; set; }
        public Product? Product { get; set; }
    }
}
