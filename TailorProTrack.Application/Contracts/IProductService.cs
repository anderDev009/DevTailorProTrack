
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Product;

namespace TailorProTrack.Application.Contracts
{
    public interface IProductService : IBaseService<ProductDtoAdd,ProductDtoRemove,ProductDtoUpdate>
    {
        Decimal GetPrice(int id);
        ServiceResult GetAllWithoutPagination();
    }
}
