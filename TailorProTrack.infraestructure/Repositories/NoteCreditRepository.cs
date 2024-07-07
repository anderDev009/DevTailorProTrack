

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
		public List<NoteCredit> SearchNoteCreditByClientId(int idClient)
		{
			var notes = context.Set<NoteCredit>().Where(x => x.FK_CLIENT == idClient).Include(x => x.Client).ToList();

			return notes;
		}
	}
}
