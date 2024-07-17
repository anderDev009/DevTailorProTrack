using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.Application.Dtos.Client;

namespace TailorProTrack.Application.Dtos.NoteCredit
{
	public record NoteCreditDtoAdd
	{
		public int FkClient { get; set; }
		public int FkPayment { get; set; }
		public decimal Amount { get; set; }

	}

	public record NoteCreditDtoUpdate
	{
		public int Id { get; set; }
		public int FkClient { get; set; }
		public int FkPayment { get; set; }
		public decimal Amount { get; set; }
	}

	public record NoteCreditDtoGet
	{
		public int Id { get; set; }
		public decimal Amount { get; set; }
		public int FkPayment { get; set; }
		public DateTime DateCreated { get; set; }

		public ClientDtoGet? Client { get; set; }
	}
}
