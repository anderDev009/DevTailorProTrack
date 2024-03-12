
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Size : BaseEntity
    {
        public string SIZE { get; set; }
        public int FKCATEGORYSIZE { get; set; }

        public CategorySize? categorySize { get; set; }
        public List<BuyInventoryDetail>? sizeInBuys { get; set; }
    }
}
