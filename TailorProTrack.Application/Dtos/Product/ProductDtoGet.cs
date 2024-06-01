using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.Application.Dtos.Size;

namespace TailorProTrack.Application.Dtos.Product
{
    public class ProductDtoGet 
    {
        public int Id { get; set; }
        public string name_prod { get; set; }
        public string description { get; set; }
        public decimal sale_price { get; set; }
        public string? type { get; set; }
        
        public List<ColorDtoGetMapped>? ColorsAsociated { get; set; }
        public List<SizeDtoGetMapped>? SizesAsociated { get; set; }
    } 
}
