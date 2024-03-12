
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Type_prod : BaseEntity
    {
        public string TYPE_PROD { get; set; }

        public List<Product>? Products { get; set; }
    }
}
