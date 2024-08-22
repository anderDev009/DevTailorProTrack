

using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
	public class AccountCredit : BaseEntity
	{
		public int FK_BANK_ACC { get; set; }
		public int FK_EXPENSE { get; set; }
		public decimal AMOUNT { get; set; }

		//nav props
		public BankAccount? BankAccount { get; set; }
		public Expenses? Expense { get; set; }
	}
}
