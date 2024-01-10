

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Payment;

namespace TailorProTrack.Application.Contracts
{
    public interface IPaymentService : IBaseService<PaymentDtoAdd,PaymentDtoRemove,PaymentDtoUpdate>
    {
        ServiceResult GetPaymentsByOrderId(int orderId);
    }
}
