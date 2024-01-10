
using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Dtos.PaymentType;
using TailorProTrack.Application.Exceptions;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationPaymentTypeExtention
    {
        public static void IsValid(this PaymentTypeDtoBase dtoBase, IConfiguration configuration)
        {

            if (string.IsNullOrEmpty(dtoBase.type))
            {
                throw new PaymentTypeServiceException(configuration["validations:chainEmpty"]);
            }
        }        
    }
}
