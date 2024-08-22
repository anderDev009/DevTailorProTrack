

using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
	public class AccountDebit : BaseEntity
	{
		public int FK_BANK_ACC { get; set; }
		public int FK_PAYMENT { get; set; }
		public decimal AMOUNT { get; set; }
		//nav props
		public BankAccount? BankAccount { get; set; }
		public Payment? Payment { get; set; }
	}
}
