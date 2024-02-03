using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TailorProTrack.Application.Contracts.Color;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.Application.Extentions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _repository;
        private readonly ILogger logger;

        public ColorService(IColorRepository colorRepository, ILogger<IColorRepository> logger,
                            IConfiguration configuration)
        {
            this._repository = colorRepository;
            this.logger = logger;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; set; }
        public ServiceResult Add(ColorDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                dtoAdd.IsColorValid(this.Configuration);


                Color colorToAdd = new Color();

                colorToAdd.COLORNAME = dtoAdd.colorname;
                colorToAdd.CODE_COLOR = dtoAdd.code;
                colorToAdd.USER_CREATED = dtoAdd.User;
                colorToAdd.CREATED_AT = dtoAdd.Date;

                int id = this._repository.Save(colorToAdd);

                result.Message = "Insertado por exito.";
                result.Data = id;
            }catch(Exception ex) 
            { 
                result.Success = false;
                result.Message = $"Error al intentar registrarlo ${ex}.";
            }
            return result;
        }

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {
                int registerCount = this._repository.GetEntities().Where(d => !d.REMOVED).Count();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);

                var colors = this._repository.GetEntitiesPaginated(@params.Page,@params.ItemsPerPage)
                                            .Where(data => !data.REMOVED)
                                            .OrderBy(data => data.ID)
                                            .Select(data => new ColorDtoGet
                                            {
                                                Id = data.ID,
                                                colorname = data.COLORNAME,
                                                code = data.CODE_COLOR
                                            })
                                            .ToList() ;

                result.Data = colors;
                result.Header = header;
                result.Message = "Obtenidos con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener los colores ${ex}.";
            }
            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var color = this._repository.GetEntity(id);

                ColorDtoGet dtoColor = new ColorDtoGet 
                { 
                    Id = color.ID,
                    colorname = color.COLORNAME,
                    code = color.CODE_COLOR
                };
                result.Data = dtoColor;
                result.Message = "Obtenido con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obetenr el color${ex}";
            }
            return result;
        }

        public ServiceResult Remove(ColorDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Color color = new Color()
                {
                    ID = dtoRemove.Id,
                    REMOVED = true,
                    USER_MOD = dtoRemove.User,
                    MODIFIED_AT = dtoRemove.Date
                };
                result.Message = "Removido con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar removerlo ${ex}";
            }
            return result;
        }

        public ServiceResult Update(ColorDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Color color = new Color()
                {
                    ID = dtoUpdate.Id,
                    USER_MOD = dtoUpdate.User,
                    CODE_COLOR = dtoUpdate.code,
                    MODIFIED_AT = dtoUpdate.Date,
                    COLORNAME = dtoUpdate.colorname,
                };
                this._repository.Update(color);
                result.Message = "Actualizado con exito";
            }catch(Exception ex)
            {
                result.Message = "Error al actualizar";
            }
            return result;
        }
    }
}
