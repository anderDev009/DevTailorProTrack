

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        bool ConfirmPayment(int idPreOrder);
    }
}
