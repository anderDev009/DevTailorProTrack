

using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace TailorProTrack.Application.Dtos.ProductSize
{
    public class ProductSizeDtoUpdate
    {
        public int Id { get; set; }
        [Required]
        public int IdSize { get; set; }
        [Required]
        public int IdProduct { get; set; }
    }
}
