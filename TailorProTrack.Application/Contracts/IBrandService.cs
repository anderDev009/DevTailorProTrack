
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Brand;

namespace TailorProTrack.Application.Contracts
{
    public interface IBrandService : IBaseService<BrandDtoAdd,BrandDtoRemove,BrandDtoUpdate>
    {
    }
}
