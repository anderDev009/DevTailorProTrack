

using TailorProTrack.Application.Core;

namespace TailorProTrack.Application.Contracts.Color
{
    public interface IColorFilterService
    {
        ServiceResult FilterByName(string name);   
        ServiceResult FilterByColorCode(string colorCode);
    }
}
