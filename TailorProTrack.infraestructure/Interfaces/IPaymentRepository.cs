

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        bool ConfirmPayment(int idPreOrder);
        decimal GetAmountPendingByIdPreOrder(int idPreOrder);
		decimal GetAmountPendingNegativeByIdPreOrder(int idPreOrder);
        List<Payment> DetailBankAccount(int idBankAccount);

		bool SaveWithNoteCredit(Payment payment);
        bool CheckIsLastPaymentPreOrder(int paymentId);
        decimal GetDebitAmount(int idAccount);
		decimal GetDebitAmountThisMonth(int idAccount);    
	}
}
