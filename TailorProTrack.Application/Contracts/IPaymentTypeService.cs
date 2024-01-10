

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.PaymentType;

namespace TailorProTrack.Application.Contracts
{
    public interface IPaymentTypeService : IBaseService<PaymentTypeDtoAdd,PaymentTypeDtoRemove,PaymentTypeDtoUpdate>
    {
    }
}
