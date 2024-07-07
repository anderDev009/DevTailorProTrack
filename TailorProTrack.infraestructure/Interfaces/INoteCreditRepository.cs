using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces;

public interface INoteCreditRepository : IBaseRepository<NoteCredit>
{
	List<NoteCredit> SearchNoteCreditByClientId(int idClient);
	List<NoteCredit> SearchNoteCreditByPaymentId(int idPayment);

}