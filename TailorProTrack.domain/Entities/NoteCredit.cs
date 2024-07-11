
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
	public class NoteCredit : BaseEntity
	{
		public int FK_CLIENT { get; set; }
		public decimal AMOUNT { get; set; }

		//navigation props
		public Client? Client { get; set; }
	}
}
