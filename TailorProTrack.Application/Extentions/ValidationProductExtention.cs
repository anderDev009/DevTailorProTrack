using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Product;
using TailorProTrack.Application.Exceptions;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationProductExtention
    {
        public static ServiceResult IsProductValid(this BaseProduct productDto, IConfiguration configuration, ITypeProdRepository typeProdRep)
        {
            ServiceResult result = new ServiceResult();
            result.Success = false;

            //campo nombre de producto
            if (string.IsNullOrEmpty(productDto.name_prod))
            {
                throw new ProductServiceException(configuration["validations:chainEmpty"]);
            }
            
            if(productDto.name_prod.Length > 255)
            {
                throw new ProductServiceException(configuration["validations:extendsCharacters"]); 
            }

            // campo de precio
            if(productDto.sale_price <= 0)
            {
                throw new ProductServiceException(configuration["validations:numberLessZero"]);
            }
            //campo fk type
            if (!typeProdRep.Exists(type => type.ID == productDto.fk_type))
            {
                throw new ProductServiceException(configuration["validations:typeDoesntExist"]);
            }
            
            result.Success = true;
            return result;
        }
    }
}
