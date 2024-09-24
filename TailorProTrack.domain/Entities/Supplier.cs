

using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string NAME_SUPPLIER { get; set; }
        public string? RNC { get; set; }
    }
}
