

using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.Application.Dtos.Product;

namespace TailorProTrack.Application.Dtos.ProductColor
{
    public class ProductColorDtoGet 
    {
        public int Id { get; set; }
        public int FkProduct { get; set; }
        public int FkColor { get; set; }
        public ProductDtoGetMapped? Product {  get; set; }
        public ColorDtoGetMapped? Color { get; set; }
    }
}
