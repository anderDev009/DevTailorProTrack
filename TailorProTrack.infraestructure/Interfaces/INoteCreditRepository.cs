using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces;

public interface INoteCreditRepository : IBaseRepository<NoteCredit>
{
	NoteCredit SearchNoteCreditByClientId(int idClient);
	void ExtractAmount(int idNoteCredit, decimal amount);
	

}