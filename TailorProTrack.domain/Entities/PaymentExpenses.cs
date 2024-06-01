
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class PaymentExpenses : BaseEntity
    {
        public int FK_EXPENSE { get; set; }
        public int FK_PAYMENT_TYPE { get; set; }
        public int? FK_BANK_ACCOUNT { get; set; }
        public decimal AMOUNT { get; set; }
        
        public Expenses? Expense { get; set; }
        public PaymentType? PaymentType { get; set; }
        public BankAccount? BankAccount { get; set;  }
    }
}
