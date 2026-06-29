using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces;

public interface INoteCreditRepository : IBaseRepository<NoteCredit>
{
	NoteCredit SearchNoteCreditByClientId(int idClient);
	NoteCredit GetByIdWithDetail(int id);
	List<(NoteCreditPayment ncp, Payment payment, PreOrder preOrder)> GetPaymentDetailsByNoteCreditId(int noteCreditId);
	void ExtractAmount(int idNoteCredit, decimal amount);
	void UpdateAmount(int FK_CLIENT);
}