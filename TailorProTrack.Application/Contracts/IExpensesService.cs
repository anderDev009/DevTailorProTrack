

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Expenses;
using TailorProTrack.Application.Service.BaseServices;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Contracts
{
    public interface IExpensesService : IBaseServiceGeneric<ExpensesDtoAdd,ExpensesDtoUpdate,ExpensesDtoGet,Expenses>
    {
        //ServiceResult GetAccountsPayable();
        ServiceResult ConfirmExpenses(int IdExpense);
        ServiceResult GetExpensesPending();
        ServiceResult GetExpensesWithIdBuy();
        ServiceResult GetExpensesWithoutBuy();
    }
}
