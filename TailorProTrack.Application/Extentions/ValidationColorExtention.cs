using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.Application.Exceptions;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationColorExtention
    {
        public static void  IsColorValid(this ColorBaseDto colorDto, IConfiguration configuration)
        {

            if (string.IsNullOrEmpty(colorDto.code) || string.IsNullOrEmpty(colorDto.colorname))
            {
                throw new ColorServiceException(configuration["validations:chainEmpty"]);
            }

        }

        public static void IsColorValidToAdd(this ColorBaseDto colorDto, IConfiguration configuration, IColorRepository colorRepository)
        {
            IsColorValid(colorDto, configuration);

            if (colorRepository.SearchEntities().Where(color => color.COLORNAME == colorDto.colorname).FirstOrDefault() != null)
            {
                throw new ColorServiceException(configuration["validations:colornameAlreadyExists"]);
            }

            if (colorRepository.SearchEntities().Where(color => color.CODE_COLOR == colorDto.code).FirstOrDefault() != null)
            {
                throw new ColorServiceException(configuration["validations:codecolorAlreadyRegistered"]);
            }
        }
    }
}
