
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class InventoryColor : BaseEntity
    {
        public int FK_COLOR_PRIMARY { get; set; }
        public int? FK_COLOR_SECONDARY { get; set; }
        public int QUANTITY {  get; set; }
        public int FK_INVENTORY { get; set; }

    }
}
