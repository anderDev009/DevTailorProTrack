
using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Size;
using TailorProTrack.Application.Exceptions;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationSizeExtention
    {

        public static ServiceResult IsSizeValid(this SizeBaseDto sizeDto, IConfiguration configuration)
        {
            ServiceResult result = new ServiceResult();
            result.Success = false;

            //en caso de que la cadena este vacia
            if (String.IsNullOrEmpty(sizeDto.size))
            {
                throw new SizeServiceException(configuration["validations:chainEmpty"]);
            }

            result.Success = true;
            return result;
        }
    }
}
