

using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Sales : BaseEntity
    {
        public int FK_PREORDER { get; set; }
        public string? COD_ISC {  get; set; }
        public decimal? ITBIS { get; set; }
        public string? B14 { get; set; }
        public bool? INVOICED { get; set; }
        public decimal TOTAL_AMOUNT { get; set; }

        public PreOrder? PreOrder { get; set; }
    }
}
