

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface ICodesDgiRepository : IBaseRepository<CodesDgi>
    {
        int UseCode();
        int ReverseCode();
    }
}
