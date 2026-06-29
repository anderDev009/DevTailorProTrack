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

	public record NoteCreditPaymentDetailDto
	{
		public int IdPayment { get; set; }
		public decimal AmountPaid { get; set; }
		public decimal OverpaidAmount { get; set; }
		public DateTime PaymentDate { get; set; }
		public int IdOrder { get; set; }
		public DateTime OrderDeliveryDate { get; set; }
	}

	public record NoteCreditDtoGetDetail
	{
		public int Id { get; set; }
		public decimal Amount { get; set; }
		public DateTime DateCreated { get; set; }
		public ClientDtoGet? Client { get; set; }
		public List<NoteCreditPaymentDetailDto> Payments { get; set; } = new();
	}
}
