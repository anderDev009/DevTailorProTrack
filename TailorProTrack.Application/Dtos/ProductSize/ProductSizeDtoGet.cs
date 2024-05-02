

using TailorProTrack.Application.Dtos.Product;
using TailorProTrack.Application.Dtos.Size;

namespace TailorProTrack.Application.Dtos.ProductSize
{
    public class ProductSizeDtoGet
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int IdSize {  get; set; }
        public ProductDtoGet? Product { get; set; }
        public SizeDtoGet? Size { get; set; }
    }
}
