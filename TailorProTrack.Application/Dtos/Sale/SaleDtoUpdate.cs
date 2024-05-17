

using System.ComponentModel.DataAnnotations;

namespace TailorProTrack.Application.Dtos.Sale
{
    public class SaleDtoUpdate : SaleDtoBase
    {
        public int Id {  get; set; }
        [Required]
        public int FkPreOrder { get; set; }
        public string? CodIsc { get; set; } = null;
        public decimal? Itbis { get; set; } = null;
        public string? B14 { get; set; } = null;
    }
}
