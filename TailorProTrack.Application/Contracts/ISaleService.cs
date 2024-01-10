

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Sale;

namespace TailorProTrack.Application.Contracts
{
    public interface ISaleService : IBaseService<SaleDtoAdd,SaleDtoRemove,SaleDtoUpdate>
    {
    }
}
