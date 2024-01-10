

namespace TailorProTrack.Application.Dtos.Payment
{
    public class PaymentDtoGet
    {
        public int IdOrder { get; set; }
        public decimal Amount { get; set; }
        public int PaymentNumbers { get; set; }
    }   
}
