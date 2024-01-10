
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.PaymentType;
using TailorProTrack.Application.Extentions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _repository;
        private readonly ILogger logger;
        public PaymentTypeService(IPaymentTypeRepository repository, ILogger<IPaymentTypeRepository> logger,
                                  IConfiguration configuration)
        {
            _repository = repository;
            this.logger = logger;
            Configuration = configuration;
        }

        private readonly IConfiguration Configuration;
        public ServiceResult Add(PaymentTypeDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                dtoAdd.IsValid(this.Configuration);
                PaymentType type = new PaymentType
                {
                    TYPE_PAYMENT = dtoAdd.type,
                    USER_CREATED = dtoAdd.User,
                };
                this._repository.Save(type);
                result.Message = $"Agregado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al agregar el metodo de pago {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var types = this._repository.GetEntities().Where(d => !d.REMOVED).Select(d => new PaymentTypeDtoGet
                {
                    Id =  d.ID,
                    Type = d.TYPE_PAYMENT
                });

                result.Data = types;
                result.Message = "Obtenidos con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los metodos de pago {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var type = this._repository.GetEntity(id);

                result.Data = type;
                result.Message = "Obtenido con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener el metodo de pago {ex.Message}";
            }
            return result;
        }

        public ServiceResult Remove(PaymentTypeDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                PaymentType type = new PaymentType
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User
                };

                this._repository.Remove(type);
                result.Message = "Removido con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al remover el metodo de pago {ex.Message}";
            }
            return result;
        }

        public ServiceResult Update(PaymentTypeDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                dtoUpdate.IsValid(this.Configuration);
                PaymentType type = new PaymentType
                {
                    ID = dtoUpdate.Id,
                    TYPE_PAYMENT = dtoUpdate.type,
                    USER_MOD = dtoUpdate.User,
                 
                };

                result.Message = "Actualizado con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar el metodo de pago {ex.Message}";
            }
            return result;
        }
    }
}
