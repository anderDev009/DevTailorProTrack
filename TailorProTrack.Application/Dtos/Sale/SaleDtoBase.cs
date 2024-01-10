
namespace TailorProTrack.Application.Dtos.Sale
{
    public class SaleDtoBase : BaseDto
    {
        public int FkOrder {  get; set; }
        public string CodIsc { get; set; }
    }
}
