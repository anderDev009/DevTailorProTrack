
using Microsoft.EntityFrameworkCore.Storage.Json;
using TailorProTrack.Application.Contracts.Color;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service.Filter
{
    public class ColorFilterService : IColorFilterService
    {
        private readonly IColorRepository _colorRepository;
        public ColorFilterService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }
        public ServiceResult FilterByColorCode(string colorCode)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var colors = _colorRepository.FilterByColorCode(colorCode).Select(color => new ColorDtoGet
                {
                    Id = color.ID,
                    colorname = color.COLORNAME,
                    code = color.CODE_COLOR
                });

                result.Data = colors;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los colores: {ex.Message}";
            }
            return result;
        }

        public ServiceResult FilterByName(string name)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var colors = _colorRepository.FilterByName(name).Select(color => new ColorDtoGet
                {
                    Id = color.ID,
                    colorname = color.COLORNAME,
                    code = color.CODE_COLOR
                });
                result.Data = colors;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los colores: {ex.Message}";
            }
            return result;
        }
    }
}
