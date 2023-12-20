using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Color;

namespace TailorProTrack.Application.Contracts
{
    public interface IColorService : IBaseService<ColorDtoAdd,ColorDtoRemove,ColorDtoUpdate>
    {
    }
}
