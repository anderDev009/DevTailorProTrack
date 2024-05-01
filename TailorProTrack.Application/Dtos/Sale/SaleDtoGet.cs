
using Microsoft.Extensions.Logging.Abstractions;

namespace TailorProTrack.Application.Dtos.Sale
{
    public class SaleDtoGet
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string? CodIsc { get; set; } = null;
        public string? B14 { get; set; } = null;
        public int IdPreOrder { get; set; }
        public decimal? Itbis { get; set; } = null;
        public int FkOrder {  get; set; }
    }
}
