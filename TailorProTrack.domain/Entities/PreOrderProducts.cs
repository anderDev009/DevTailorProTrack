

using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class PreOrderProducts : BaseEntity
    {
        public int FK_PREORDER {  get; set; }
        public int FK_PRODUCT { get; set; }
        public int FK_SIZE { get; set; }
        public int QUANTITY { get; set; }
        public int COLOR_PRIMARY { get; set; }
        public int? COLOR_SECONDARY { get; set; }

        public PreOrder? PreOrder { get; set; }
        public Size? Size { get; set; }
        public Product? Product {  get; set; }
        public Color? ColorPrimary {  get; set; }
        public Color? ColorSecondary { get; set; }
    }
}
    