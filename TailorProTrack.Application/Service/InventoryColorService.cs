

using Microsoft.Extensions.Logging;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.InventoryColor;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class InventoryColorService : IInventoryColorService
    {
        private readonly IInventoryColorRepository _repository;
        private  readonly ILogger _logger;

        public InventoryColorService(IInventoryColorRepository inventoryColorRepository,
                                     ILogger<IInventoryColorRepository> logger)
        {
            this._repository = inventoryColorRepository;
            this._logger = logger;
        }

        public ServiceResult Add(InventoryColorDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                InventoryColor inventoryColor = new InventoryColor
                {
                    FK_INVENTORY = dtoAdd.fk_inventory,
                    FK_COLOR_PRIMARY = dtoAdd.fk_color_primary,
                    FK_COLOR_SECONDARY = dtoAdd.fk_color_secondary,
                    QUANTITY = dtoAdd.quantity,
                    CREATED_AT = dtoAdd.Date,
                    USER_CREATED = dtoAdd.User
                };
                this._repository.Save(inventoryColor);

                result.Message = "Registrado con exito";
            }catch(Exception ex) 
            {
                result.Success = false;
                result.Message = $"Error al registrar: {ex}";
            }
            return result;
        }

        public ServiceResult GetAll()
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Remove(InventoryColorDtoRemove dtoRemove)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Update(InventoryColorDtoUpdate dtoUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
