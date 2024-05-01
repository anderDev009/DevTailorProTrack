

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Sale;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Contracts
{
    public interface ISaleService : IBaseServiceGeneric<SaleDtoAdd,SaleDtoRemove,SaleDtoUpdate,Sales>
    {
    }
}
