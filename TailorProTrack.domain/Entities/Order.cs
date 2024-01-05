

using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Order : BaseEntity
    {
        public int FK_USER {  get; set; }
        public int FK_CLIENT { get; set; }
        public bool CHECKED { get; set; }
        public decimal AMOUNT { get; set; }

        public string DESCRIPTION_JOB {  get; set; }
        public string? STATUS_ORDER { get; set; }
    }
}
