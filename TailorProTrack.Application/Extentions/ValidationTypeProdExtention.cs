

using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.TypeProd;
using TailorProTrack.Application.Exceptions;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationTypeProdExtention
    {
        public static ServiceResult IsTypeProdValid(this BaseTypeProd typeProdDto, IConfiguration configuration)
        {
            ServiceResult result = new ServiceResult();
            result.Success = false;

            if (string.IsNullOrEmpty(typeProdDto.typeProd))
            {
                throw new TypeProdServiceException(configuration["validations:chainEmpty"]);
            }

            result.Success = true;
            return result;
        }
    }
}
