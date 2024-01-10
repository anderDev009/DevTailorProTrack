

using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Sales : BaseEntity
    {
        public int FK_ORDER { get; set; }
        public string COD_ISC {  get; set; }
        public decimal TOTAL_AMOUNT { get; set; }
    }
}
