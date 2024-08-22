using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces;

public interface IPaymentExpensesRepository : IBaseRepository<PaymentExpenses>
{
	decimal GetCreditAmountTotal(int idAccount);
	decimal GetCreditThisMonth(int idAccount);

}