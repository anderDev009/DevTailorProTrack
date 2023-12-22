

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

        //repositorio de color
        private readonly IColorService _colorService;

        //repositorios
        //inventario 
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryColorService(IInventoryColorRepository inventoryColorRepository,
                                     ILogger<IInventoryColorRepository> logger,
                                     IInventoryRepository inventoryRepository,
                                     IColorService colorService)
        {
            this._repository = inventoryColorRepository;
            this._inventoryRepository = inventoryRepository;
            this._logger = logger;
            this._colorService = colorService;
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
                    FK_COLOR_SECONDARY = dtoAdd.fk_color_secondary == 0 ? null : dtoAdd.fk_color_secondary,
                    QUANTITY = dtoAdd.quantity,
                    CREATED_AT = dtoAdd.Date,
                    USER_CREATED = dtoAdd.User
                };
                int id  = this._repository.Save(inventoryColor);

                result.Data = id;
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

                int id  = this.Add(dtoAdd).Data;
                //actualizando el inventario
                Inventory inventory = this._inventoryRepository.GetEntity(dtoAdd.fk_inventory);
                inventory.QUANTITY = this.GetQuantityTotalById(dtoAdd.fk_inventory).Data ;
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

        public ServiceResult GetByIdInventory(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var colors = this._repository.GetEntities().Where(data => !data.REMOVED &&
                                                                  data.FK_INVENTORY == id)

                                                                  .Select(data => new InventoryColorDtoGet
                                                                  {
                                                                      colorPrimary = this._colorService.GetById(data.FK_COLOR_PRIMARY).Data,
                                                                      colorSecondary = (data.FK_COLOR_SECONDARY.HasValue) ? this._colorService.GetById(data.FK_COLOR_SECONDARY.Value).Data : 0,
                                                                      quantity = data.QUANTITY
                                                                  }) ;

                result.Data = colors;
                result.Message = "Obtenidos con exito";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener por el ID de inventario {ex}";
            }
            return result;
        }

        public ServiceResult GetQuantityTotalById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var quantity = this._repository.GetEntities().GroupBy(d=> d.FK_INVENTORY).Select(data => new
                                                           {
                                                            data.Key,   
                                                             total = data.Sum(n => n.QUANTITY)
                                                            }).Where(d=> d.Key == id);
                                                           ;
                result.Data = quantity.Sum(d=>d.total);
                result.Message = "Cantidad obtenida con exito";
            }catch(Exception ex)
            {

            }

            return result;
        }

        public ServiceResult Remove(InventoryColorDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                InventoryColor inventoryToRemove = new InventoryColor
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User,
                    MODIFIED_AT = dtoRemove.Date
                };
                this._repository.Remove(inventoryToRemove);
            }catch(Exception ex)
            {

            }
            return result;
        }

        public ServiceResult Update(InventoryColorDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try 
            { 
                InventoryColor inventoryToUpdate = new InventoryColor
                {
                    ID = dtoUpdate.Id,
                    FK_COLOR_PRIMARY = dtoUpdate.fk_color_primary,
                    FK_COLOR_SECONDARY = dtoUpdate.fk_color_secondary,
                    USER_MOD = dtoUpdate.User
                };

                this._repository.Update(inventoryToUpdate);
                result.Message = "Actualizado con exito";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizarlo {ex}";
            }
            return result;
        }
    }
}
