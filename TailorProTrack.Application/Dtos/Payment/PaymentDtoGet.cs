

using TailorProTrack.Application.Dtos.Client;

namespace TailorProTrack.Application.Dtos.Payment
{
    public class PaymentDtoGet
    {
        public int IdOrder { get; set; }
        public decimal Amount { get; set; }
        public string? AccountPayment { get; set; }

        public int PaymentNumbers { get; set; }
        public ClientDtoGet? Client { get; set; }
    }   
}
