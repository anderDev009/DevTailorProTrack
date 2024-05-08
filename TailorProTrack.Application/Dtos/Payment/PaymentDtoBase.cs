

namespace TailorProTrack.Application.Dtos.Payment
{
    public class PaymentDtoBase : BaseDto
    {
        public int FkOrder { get; set; }
        public int FkTypePayment { get; set; }
        public string? AccountPayment {  get; set; }
        public int  FkBankAccount {get; set;}
        public decimal Amount { get; set; }
    }
}
