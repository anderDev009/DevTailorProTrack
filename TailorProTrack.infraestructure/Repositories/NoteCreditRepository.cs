

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
				noteCredit.AMOUNT += entity.AMOUNT;
				Update(entity);
				return noteCredit.ID;
			}

			return base.Save(entity);
		}

		public NoteCredit SearchNoteCreditByClientId(int idClient)
		{
			var notes = context.Set<NoteCredit>().Where(x => x.FK_CLIENT == idClient).Include(x => x.Client).FirstOrDefault();

			return notes;
		}

		public void ExtractAmount(int idNoteCredit, decimal amount)
		{
			var note = GetEntity(idNoteCredit);
			note.AMOUNT -= amount;
		
		}
	}
}
