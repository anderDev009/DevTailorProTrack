

using TailorProTrack.Application.Contracts.Size;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Size;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service.Filter
{
    public class SizeFilterService : ISizeFilterService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeFilterService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;                
        }

        public ServiceResult FilterByIdCategory(int categoryId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var sizes = _sizeRepository.FilterByIdCategory(categoryId).Select(data => new SizeDtoGet
                {
                    Id = data.ID,
                    Size = data.SIZE,
                    Category = data.CategorySize.CATEGORY
                });

                result.Data = sizes;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los sizes: {ex.Message}";
            }

            return result;
        }

        public ServiceResult FilterByName(string name)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var sizes = _sizeRepository.FilterByName(name).Select(data => new SizeDtoGet
                {
                    Id = data.ID,
                    Size = data.SIZE,
                    Category = data.CategorySize.CATEGORY
                }); ;

                result.Data = sizes;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los sizes: {ex.Message}";
            }

            return result;
        }
    }
}
