

using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class PaymentType : BaseEntity
    {
        public string TYPE_PAYMENT {  get; set; }

        public List<PaymentExpenses>? PaymentExpensesList { get; set; }
    }
}
