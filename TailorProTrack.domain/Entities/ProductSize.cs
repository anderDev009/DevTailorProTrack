
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class ProductSize : BaseEntity
    {
        public int FK_SIZE {  get; set; }
        public int FK_PRODUCT {  get; set; }    

        public Size? Size { get; set; }
        public Product? Product { get; set; }
    }
}
