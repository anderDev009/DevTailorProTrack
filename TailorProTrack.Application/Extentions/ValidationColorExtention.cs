using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.Application.Exceptions;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationColorExtention
    {
        public static void  IsColorValid(this ColorBaseDto colorDto, IConfiguration configuration)
        {
            ServiceResult result = new ServiceResult();

            if (string.IsNullOrEmpty(colorDto.code) || string.IsNullOrEmpty(colorDto.colorname))
            {
                throw new ColorServiceException(configuration["validations:chainEmpty"]);
            }

        }
    }
}
