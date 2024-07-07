
using AutoMapper;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.NoteCredit;
using TailorProTrack.Application.Service.BaseServices;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
	public class NoteCreditService(IMapper mapper, INoteCreditRepository repository) :
		GenericService<NoteCreditDtoAdd, NoteCreditDtoUpdate, NoteCreditDtoGet, NoteCredit>(mapper, repository),
		INoteCreditService
	{
		public ServiceResult SearchNoteCreditByClientId(int idClient)
		{
			ServiceResult result = new ServiceResult();
			try
			{
				var notes = repository.SearchNoteCreditByClientId(idClient);
				result.Data = mapper.Map<List<NoteCreditDtoGet>>(notes);
				result.Message = "Obtenidos con exito";
			}
			catch (Exception e)
			{
				result.Success = false;
				result.Message = $"Error: {e.Message}";
			}

			return result;
		}
	}
}
