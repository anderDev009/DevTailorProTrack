
using AutoMapper;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Expenses;
using TailorProTrack.Application.Service.BaseServices;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class ExpensesService : GenericService<ExpensesDtoAdd, ExpensesDtoUpdate, ExpensesDtoGet, Expenses>, IExpensesService
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IMapper _mapper;
        public ExpensesService(IExpensesRepository expensesRepository, IMapper mapper) : base(mapper,expensesRepository)
        {
            _expensesRepository = expensesRepository;
            _mapper = mapper;
        }

        public ServiceResult GetAccountsPayable()
        {
            ServiceResult result = new();
            try
            {
                var report = _expensesRepository.GetAccountsPayable();
                result.Data = _mapper.Map<List<ExpensesDtoAdd>>(report);
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error {ex.Message}";
            }
            return result;
        }
    }
}
