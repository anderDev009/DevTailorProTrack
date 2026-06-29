

using AutoMapper;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Expenses.PaymentExpense;
using TailorProTrack.Application.Service.BaseServices;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class PaymentExpenseService : GenericService<PaymentExpenseDtoAdd, PaymentExpenseDtoUpdate, PaymentExpenseDtoGet, PaymentExpenses>, IPaymentExpensesService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentExpensesRepository _paymentExpensesRepository;

        public PaymentExpenseService(IPaymentExpensesRepository repository, IMapper mapper):base(mapper,repository)
        {
            _mapper = mapper;
            _paymentExpensesRepository = repository;
        }

        public ServiceResult Void(int id)
        {
            ServiceResult result = new();
            try
            {
                _paymentExpensesRepository.Void(id);
                result.Message = "Pago anulado con éxito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al anular el pago: {ex.Message}";
            }
            return result;
        }
    }

}
