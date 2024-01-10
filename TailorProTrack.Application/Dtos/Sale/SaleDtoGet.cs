
namespace TailorProTrack.Application.Dtos.Sale
{
    public class SaleDtoGet
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string CodIsc { get; set; }
        public int FkOrder {  get; set; }
    }
}
