

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
    }
}
    