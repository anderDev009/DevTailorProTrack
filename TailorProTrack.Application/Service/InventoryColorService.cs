

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

        //repositorios
        //inventario 
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryColorService(IInventoryColorRepository inventoryColorRepository,
                                     ILogger<IInventoryColorRepository> logger,
                                     IInventoryRepository inventoryRepository)
        {
            this._repository = inventoryColorRepository;
            this._inventoryRepository = inventoryRepository;
            this._logger = logger;
        }

        public ServiceResult Add(InventoryColorDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
               
                InventoryColor inventoryColor = new InventoryColor
                {
                    FK_INVENTORY = 16,
                    FK_COLOR_PRIMARY = dtoAdd.fk_color_primary,
                    FK_COLOR_SECONDARY = dtoAdd.fk_color_secondary == 0 ? null : dtoAdd.fk_color_secondary,
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

        public ServiceResult AddAndUpdateInventory(InventoryColorDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                InventoryColor invColor = new InventoryColor
                {
                    FK_INVENTORY = dtoAdd.fk_inventory,
                    FK_COLOR_PRIMARY = dtoAdd.fk_color_primary,
                    FK_COLOR_SECONDARY = dtoAdd.fk_color_secondary,
                    QUANTITY = dtoAdd.quantity,
                    CREATED_AT= dtoAdd.Date,
                    USER_CREATED = dtoAdd.User
                };
                int id = this._repository.Save(invColor);
                //actualizando el inventario
                Inventory inventory = this._inventoryRepository.GetEntity(dtoAdd.fk_inventory);
                inventory.QUANTITY = this.GetQuantityTotalById(dtoAdd.fk_inventory).Data;
                this._inventoryRepository.Update(inventory);
                //enviando el mensaje
                result.Message = "Insertado con exito";
                result.Data = id;
            }catch(Exception ex )
            {
                result.Success = false;
                result.Message = $"Error al agregar {ex}";
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

        public ServiceResult GetQuantityTotalById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                int quantity = this._repository.GetEntities().Select(data => new
                                                           {
                                                             data.FK_INVENTORY,
                                                             data.QUANTITY
                                                            }).Where(data => data.FK_INVENTORY == id)
                                                            .Sum(data=>data.QUANTITY);
                result.Data = quantity;
                result.Message = "Cantidad obtenida con exito";
            }catch(Exception ex)
            {

            }

            return result;
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
