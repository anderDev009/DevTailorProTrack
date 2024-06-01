

using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Dtos.Sale;
using TailorProTrack.Application.Exceptions;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationSalesExtention
    {
        public static void IsValid(this SaleDtoBase dtoBase,IConfiguration configuration, IPreOrderRepository preorderRepository)
        {
            if (preorderRepository.Exists(preorder => preorder.ID == dtoBase.FkOrder))
            {
                throw new SaleServiceException(configuration["validations:preorderDoesntExist"]);
            }
        }
    }
}
