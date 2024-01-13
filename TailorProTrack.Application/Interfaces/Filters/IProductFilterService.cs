

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Product;

namespace TailorProTrack.Application.Interfaces.Filters
{
    public interface IProductFilterService
    {
        ServiceResult SearchByProductName (string productName);
        ServiceResult GetByMinorPrice(decimal price);
        ServiceResult GetByHigherPrice (decimal price);

        ServiceResult SearchByType(int fkType);

    }
}
