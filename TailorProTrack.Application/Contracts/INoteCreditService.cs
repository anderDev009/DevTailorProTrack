

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.NoteCredit;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Contracts
{
	public interface INoteCreditService : IBaseServiceGeneric<NoteCreditDtoAdd,NoteCreditDtoUpdate,NoteCreditDtoGet,NoteCredit>
	{
		ServiceResult SearchNoteCreditByClientId(int idClient);
	}
}
