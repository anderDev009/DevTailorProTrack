
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Inventory : BaseEntity
    {
        public int FK_SIZE { get; set; }
        public int QUANTITY_AVAILABLE { get; set; }
        public DateTime LAST_REPLENISHMENT {  get; set; } 
    }
}
