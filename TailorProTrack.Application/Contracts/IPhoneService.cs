

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Phone;

namespace TailorProTrack.Application.Contracts
{
    public interface IPhoneService :IBaseService<PhoneDtoAdd,PhoneDtoRemove,PhoneDtoUpdate>
    {
    }
}
