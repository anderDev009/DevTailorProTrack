

using TailorProTrack.Application.Dtos.Product;
using TailorProTrack.Application.Dtos.Size;

namespace TailorProTrack.Application.Dtos.PreOrder
{
    public class PreOrderDtoGet
    {
        public int FK_PREORDER { get; set; }
        public int FK_PRODUCT { get; set; }
        public int FK_SIZE { get; set; }
        public int QUANTITY { get; set; }
        public int COLOR_PRIMARY { get; set; }
        public int? COLOR_SECONDARY { get; set; }

        public PreOrderDtoGetMapped? PreOrder { get; set; }
        public SizeDtoGetMapped? Size { get; set; }
        public ProductDtoGetMapped? Product { get; set; }
    }
}
