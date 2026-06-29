

using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
	public class NoteCreditRepository(TailorProTrackContext context)
		: BaseRepository<NoteCredit>(context), INoteCreditRepository
	{
		public override int Save(NoteCredit entity)
		{
			// verificar si existe una nota de credito para el cliente
			bool exist = context.Set<NoteCredit>().Any(x => x.FK_CLIENT == entity.FK_CLIENT);
			if (exist)
			{
				var noteCredit = context.Set<NoteCredit>().First(x => x.FK_CLIENT == entity.FK_CLIENT);
				return noteCredit.ID;
			}
			entity.AMOUNT = 0;
			return base.Save(entity);
		}

		public NoteCredit SearchNoteCreditByClientId(int idClient)
		{
			var notes = context.Set<NoteCredit>().Where(x => x.FK_CLIENT == idClient).Include(x => x.Client).FirstOrDefault();

			return notes;
		}

		public NoteCredit GetByIdWithDetail(int id)
		{
			return context.Set<NoteCredit>()
				.Include(x => x.Client)
				.FirstOrDefault(x => x.ID == id);
		}

		public List<(NoteCreditPayment ncp, Payment payment, PreOrder preOrder)> GetPaymentDetailsByNoteCreditId(int noteCreditId)
		{
			return (from ncp in context.Set<NoteCreditPayment>()
					join p in context.Set<Payment>() on ncp.FK_PAYMENT equals p.ID
					join po in context.Set<PreOrder>() on p.FK_ORDER equals po.ID
					where ncp.FK_CREDIT == noteCreditId
					select new { ncp, p, po })
				.AsEnumerable()
				.Select(x => (x.ncp, x.p, x.po))
				.ToList();
		}

		public void ExtractAmount(int idNoteCredit, decimal amount)
		{
			var note = GetEntity(idNoteCredit);
			note.AMOUNT -= amount;
			context.Set<NoteCredit>().Update(note);
			context.SaveChanges();
		}

		public void UpdateAmount(int FK_CLIENT)
		{
			var noteCredit = context.Set<NoteCredit>().First(x => x.FK_CLIENT == FK_CLIENT);
			noteCredit.AMOUNT = context.Set<NoteCreditPayment>()
				.Where(x => x.FK_CREDIT == noteCredit.ID)
				.Sum(x => x.AMOUNT);
			Update(noteCredit);
		}
	}
}
