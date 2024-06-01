
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.Application.Dtos.Product;
using TailorProTrack.Application.Dtos.Size;

namespace TailorProTrack.Application.Dtos.PreOrderProducts
{
    public class PreOrderProductDtoGetMapped
    {
        public int ID { get; set; }
        public int FK_PREORDER { get; set; }
        public int FK_PRODUCT { get; set; }
        public int FK_SIZE { get; set; }
        public int QUANTITY { get; set; }
        public int COLOR_PRIMARY { get; set; }
        public int? COLOR_SECONDARY { get; set; }
        public decimal? CUSTOM_PRICE { get; set; }
        public SizeDtoGetMapped? Size {  get; set; }
        public ProductDtoGetMapped? Product { get; set; }
        public ColorDtoGetMapped? ColorPrimary {  get; set; }
        public ColorDtoGetMapped? ColorSecondary {  get; set; }
    }
}
