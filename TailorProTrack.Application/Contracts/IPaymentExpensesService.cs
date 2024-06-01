using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Expenses.PaymentExpense;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Contracts
{
    public interface IPaymentExpensesService : IBaseServiceGeneric<PaymentExpenseDtoAdd,PaymentExpenseDtoUpdate,PaymentExpenseDtoGet,PaymentExpenses
    >
    {
    }
}
