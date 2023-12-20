
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Inventory : BaseEntity
    {
        public int FK_SIZE {  get; set; }   
        public int FK_PRODUCT { get; set; } 
        public int QUANTITY {  get; set; }
    }
}
