

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
      
    }

}
