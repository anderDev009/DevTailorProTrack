using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.TypeProd;
using TailorProTrack.Application.Extentions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class TypeProdService : ITypeProdService
    {
        private readonly ITypeProdRepository _repository;
        private readonly ILogger logger;

        public TypeProdService(ITypeProdRepository repository,ILogger<ITypeProdRepository> logger,
                               IConfiguration configuration)
        {
            this._repository = repository;
            this.logger = logger;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        public ServiceResult Add(TypeProdDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult(); 
            try 
            {
                result = dtoAdd.IsTypeProdValid(this.Configuration);

                if (!result.Success)
                {
                    return result;
                } 

                Type_prod type = new Type_prod
                {
                    TYPE_PROD = dtoAdd.typeProd,
                    CREATED_AT = dtoAdd.Date,
                    USER_CREATED = dtoAdd.User
                };
                this._repository.Save(type);
                result.Message = "Tipo registrado con exito";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al registrar el tipo: ${ex}";
            }
            return result;
        }

        public ServiceResultWithHeader  GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {
                int registerCount = this._repository.GetEntities().Where(d => !d.REMOVED).Count();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);


                var types = this._repository.GetEntities()
                                             .Where(type => !type.REMOVED)
                                             .Select(data => new TypeProdDtoGet
                                             {
                                                 Id = data.ID,
                                                 Type =  data.TYPE_PROD
                                             })
                                             .Skip((@params.Page - 1) * @params.ItemsPerPage)
                                             .Take(@params.ItemsPerPage)
                                             .ToList();
                
                result.Message = "Tipos obtenidos con exito";
                result.Header = header;
                result.Data = types;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los tipos: ${ex}";
            }
            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Type_prod type = this._repository.GetEntity(id);
               
                result.Message = "Tipo obtenido con exito";
                result.Data = type;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener el tipo: ${ex}";
            }
            return result;
        }

        public ServiceResult Remove(TypeProdDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Type_prod type = new Type_prod
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User,
                    MODIFIED_AT = dtoRemove.Date
                };
                this._repository.Remove(type);
                result.Message = "Tipo removido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar eliminar el tipo: ${ex}";
            }
            return result;
        }

        public ServiceResult Update(TypeProdDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Type_prod type = new Type_prod
                {
                    ID = dtoUpdate.Id,
                    TYPE_PROD = dtoUpdate.typeProd,
                    MODIFIED_AT =  dtoUpdate.Date,
                    USER_MOD = dtoUpdate.User
                };

                this._repository.Update(type);
                result.Message = "Tipo actualizado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar el tipo: ${ex}";
            }
            return result;
        }
    }
}
