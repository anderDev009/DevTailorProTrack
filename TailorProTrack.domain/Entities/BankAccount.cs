
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class BankAccount : BaseEntity   
    {
        public string BANK_ACCOUNT {  get; set; }
        public int FK_BANK {  get; set; }
        public decimal BALANCE { get; set; }
        public decimal CREDIT_AMOUNT { get; set; }
        public decimal DEBIT_AMOUNT { get; set; }

		//references
		public List<PaymentExpenses>? PaymentExpenses { get; set; }
		//
		public List<AccountCredit>? AccountCredit { get; set; }
        public List<AccountDebit>? AccountDebit { get; set; }
	}
}
