

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.CodeDgi;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Contracts
{
    public interface ICodeDgiService : IBaseServiceGeneric<CodeDgiDtoAdd, CodeDgiDtoUpdate,CodeDgiDtoGet,CodesDgi>
    {
        int UseCodeDgi();
        int ReverseCodeDgi();
    }
}
