

using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;

namespace TailorProTrack.Application.Dtos.Sale
{
    public class SaleDtoAdd 
    {
        [Required]
        public int FkOrder { get; set; }
        public string? CodIsc { get; set; } = null;
        public decimal? Itbis { get; set; } = null;
        public string? B14 { get; set; } = null;
        
    }
}
