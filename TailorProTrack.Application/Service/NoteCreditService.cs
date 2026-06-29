
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

		public ServiceResult GetDetail(int id)
		{
			ServiceResult result = new ServiceResult();
			try
			{
				var note = repository.GetByIdWithDetail(id);
				if (note == null)
				{
					result.Success = false;
					result.Message = "Nota de crédito no encontrada.";
					return result;
				}

				var paymentDetails = repository.GetPaymentDetailsByNoteCreditId(id);

				var dto = new NoteCreditDtoGetDetail
				{
					Id = note.ID,
					Amount = note.AMOUNT,
					DateCreated = note.CREATED_AT,
					Client = mapper.Map<Dtos.Client.ClientDtoGet>(note.Client),
					Payments = paymentDetails.Select(x => new NoteCreditPaymentDetailDto
					{
						IdPayment = x.payment.ID,
						AmountPaid = x.payment.AMOUNT,
						OverpaidAmount = x.ncp.AMOUNT,
						PaymentDate = x.ncp.CREATED_AT,
						IdOrder = x.preOrder.ID,
						OrderDeliveryDate = x.preOrder.DATE_DELIVERY,
					}).ToList()
				};

				result.Data = dto;
				result.Message = "Obtenido con exito";
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
