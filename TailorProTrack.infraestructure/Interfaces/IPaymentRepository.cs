

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        bool ConfirmPayment(int idPreOrder);
        decimal GetAmountPendingByIdPreOrder(int idPreOrder);
		decimal GetAmountPendingNegativeByIdPreOrder(int idPreOrder);

		bool SaveWithNoteCredit(Payment payment);

        decimal GetDebitAmount(int idAccount);
		decimal GetDebitAmountThisMonth(int idAccount);    
	}
}
