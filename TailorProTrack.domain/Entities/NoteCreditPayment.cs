
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
	public class NoteCreditPayment : BaseEntity
	{
		public int FK_PAYMENT { get; set; }
		public int FK_CREDIT { get; set; }
		public decimal AMOUNT { get; set; }
	}
}
