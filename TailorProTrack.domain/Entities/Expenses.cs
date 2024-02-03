

using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Expenses : BaseEntity
    {
        public string NAME { get; set; }
        public string DESCR { get; set; }
        public decimal AMOUNT { get; set; }
        public string VOUCHER { get; set; }
    }
}
