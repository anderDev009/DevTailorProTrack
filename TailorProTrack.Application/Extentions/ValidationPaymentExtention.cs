using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Payment;
using TailorProTrack.Application.Exceptions;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationPaymentExtention
    {
        public static ServiceResult IsValid(this PaymentDtoBase dtoBase,
                                            IConfiguration configuration,
                                            IPaymentTypeRepository typePaymentRepository,
                                            IPreOrderRepository preOrderRepository) 
        {
            ServiceResult result = new ServiceResult();
            result.Success = false;

            //---
            if (decimal.IsNegative(dtoBase.Amount))
            {
                throw new PaymentServiceException(configuration["validations:numberLessZero"]);
            }
            //---
            if (!typePaymentRepository.Exists(type => type.ID == dtoBase.FkTypePayment))
            {
                throw new PaymentServiceException(configuration["validations:typeDoesntExist"]);
            }
         
            //---
            if(preOrderRepository.Exists(order => order.ID == dtoBase.FkOrder))
            {
                throw new PaymentServiceException(configuration["validations:typeDoesntExist"]);
            }
            result.Success = true;
            return result;
        }
    }
}
