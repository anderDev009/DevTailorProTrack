

using System.ComponentModel.DataAnnotations;

namespace TailorProTrack.Application.Dtos.ProductSize
{
    public class ProductSizeDtoAdd
    {
        [Required]
        public int IdSize {  get; set; }
        [Required]
        public int IdProduct {  get; set; }
    }
}
