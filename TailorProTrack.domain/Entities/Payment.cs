
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Payment : BaseEntity
    {
        public int FK_ORDER {  get; set; }
        public int FK_TYPE_PAYMENT { get; set; }
        public decimal AMOUNT { get; set; }
    }
}
