
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Size;
using TailorProTrack.Application.Exceptions;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationSizeExtention
    {

        public static void IsSizeValid(this SizeBaseDto sizeDto, IConfiguration configuration, ICategorySizeRepository _categoryRepository)
        {

            //en caso de que la cadena este vacia
            if (String.IsNullOrEmpty(sizeDto.size))
            {
                throw new SizeServiceException(configuration["validations:chainEmpty"]);
            }

            //
            if (!_categoryRepository.Exists(d => d.ID == sizeDto.FkCategory))
            {
                throw new SizeServiceException(configuration["validations:typeDoesntExist"]);
            }
        }
        public static void IsSizeValidToAdd(this SizeBaseDto sizeDto, IConfiguration configuration,
            ICategorySizeRepository _categoryRepository, ISizeRepository sizeRepository)
        {
            IsSizeValid(sizeDto, configuration, _categoryRepository);
            if (sizeRepository.SearchEntities().Where(size => size.SIZE == sizeDto.size).Count() == 0)
            {
                throw new SizeServiceException(configuration["validations:sizeAlreadyExist"]);
            }
        }
        //public static void IsFkCategoryValid(this SizeBaseDto sizeDto, IConfiguration configuration, 
        //                                    ICategorySizeRepository categoryRepository)
        //{

        //    if (!categoryRepository.Exists(data => data.ID == sizeDto.FkCategory){ }

        //    throw new SizeServiceException(configuration["validations:typeDoesntExist"]);


        //}
    }
}
