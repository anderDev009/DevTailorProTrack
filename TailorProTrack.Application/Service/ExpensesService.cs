
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

        //public ServiceResult GetAccountsPayable()
        //{
        //    ServiceResult result = new();
        //    try
        //    {
        //        var report = _expensesRepository.GetAccountsPayable();
        //        result.Data = _mapper.Map<List<ExpensesDtoGet>>(report);
        //        result.Message = "Obtenidos con exito";
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.Message = $"Error {ex.Message}";
        //    }
        //    return result;
        //}

        public ServiceResult ConfirmExpenses(int IdExpense)
        {
            ServiceResult result = new();
            try
            {
                _expensesRepository.ConfirmExpenses(IdExpense);
                result.Message = "Confirmado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetExpensesPending()
        {
            ServiceResult result = new();
            try
            {
                var expenses = _expensesRepository.GetExpensesPending();
                var expensesMapped = _mapper.Map<List<ExpensesDtoGet>>(expenses);
                foreach (var item in expensesMapped)
                {
					item.AmountPending = _expensesRepository.GetAmountPending(item.Id);
				}

                result.Data = expensesMapped;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Message = $"Error {ex.Message}";
            }

            return result;
        }

        public ServiceResult GetExpensesWithIdBuy()
        {
            ServiceResult result = new();
            try
            {
                var expenses = _expensesRepository.GetExpensesWithBuyId();
                var expensesMapped = _mapper.Map<List<ExpensesDtoGet>>(expenses);


                result.Data = expensesMapped;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetExpensesWithoutBuy()
        {
            ServiceResult result = new();
            try
            {
                var results = _expensesRepository.GetExpensesWithoutBuyId();
                result.Data = _mapper.Map<List<ExpensesDtoGet>>(results);
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
