
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class OrderProduct : BaseEntity
    {
        public int FK_ORDER {  get; set; }  
        public int FK_INVENTORYCOLOR { get; set; }
        public int QUANTITY {  get; set; }

    }
}
