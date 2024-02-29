
using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Dtos.OrderProduct;
using TailorProTrack.Application.Exceptions;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationOrderProductExtention
    {
        public static void IsValid(this OrderProductDtoBase dtoBase, IConfiguration configuration,
                                   IInventoryColorRepository invColorRepository, IOrderRepository orderRepository)
        {
            //
            if (dtoBase.Quantity <= 0)
            {
                throw new OrderProductServiceException(configuration["validations:numberLessZero"]);
            }

            if(!orderRepository.Exists(ordr => ordr.ID == dtoBase.FkOrder))
            {
                throw new OrderProductServiceException(configuration["validations:orderDoesntExist"]);
            }

            if(!invColorRepository.Exists(invColor => invColor.ID == dtoBase.FkInventoryColor))
            {
                throw new OrderProductServiceException(configuration["validations:typeDoesntExist"]);

            }

        }
    }
}
