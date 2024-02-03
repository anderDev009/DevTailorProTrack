

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Expenses;

namespace TailorProTrack.Application.Contracts
{
    public interface IExpensesService : IBaseService<ExpensesDtoAdd,ExpensesDtoRemove,ExpensesDtoUpdate>
    {
    }
}
