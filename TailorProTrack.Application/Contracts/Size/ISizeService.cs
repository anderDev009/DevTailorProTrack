using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Size;

namespace TailorProTrack.Application.Contracts.Size
{
    public interface ISizeService : IBaseService<SizeDtoAdd, SizeDtoRemove, SizeDtoUpdate>
    {
        ServiceResult GetSizesAvailablesProductById(int id);

        ServiceResult GetSizesByCategoryId(int categoryId);
        ServiceResult GetSizesAsociatedByProdId(int prodId);
    }
}
