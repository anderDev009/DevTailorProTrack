

using TailorProTrack.Application.Core;

namespace TailorProTrack.Application.Contracts.Size
{
    public interface ISizeFilterService
    {
        ServiceResult FilterByName(string name);
        ServiceResult FilterByIdCategory(int categoryId);
    }
}
