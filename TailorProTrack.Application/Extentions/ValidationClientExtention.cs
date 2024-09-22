using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Dtos.Client;
using TailorProTrack.Application.Exceptions;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationClientExtention
    {
        public static void IsValid(this ClientDtoBase dtoBase,IConfiguration configuration)
        {
            if (string.IsNullOrEmpty(dtoBase.F_name))
            {
                throw new ClientServiceException(configuration["validations:chainEmpty"]);
            }

            if (dtoBase.F_name.Length > 30) throw new ClientServiceException(configuration["validations:extendsCharacters"]);
            if (dtoBase.L_name.Length > 30) throw new ClientServiceException(configuration["validations:extendsCharacters"]);
            if (dtoBase.F_surname.Length > 30) throw new ClientServiceException(configuration["validations:extendsCharacters"]);
            if (dtoBase.L_surname.Length > 30) throw new ClientServiceException(configuration["validations:extendsCharacters"]);
            //DNI
            if (dtoBase.Dni.Length > 20) throw new ClientServiceException(configuration["validations:extendsCharacters"]);
            //RNC
            if (dtoBase.Rnc.Length > 12) throw new ClientServiceException(configuration["validations:extendsCharacters"]);

        }
    }
}
