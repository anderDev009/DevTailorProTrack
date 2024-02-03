

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Contracts.Color;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.InventoryColor;
using TailorProTrack.Application.Extentions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TailorProTrack.Application.Service
{
    public class InventoryColorService : IInventoryColorService
    {
        private readonly IInventoryColorRepository _repository;
        private  readonly ILogger _logger;

        //service de color
        private readonly IColorService _colorService;
        //repositorios
        //color
        private readonly IColorRepository _colorRepository;
        //inventario 
        private readonly IInventoryRepository _inventoryRepository;
        //producto
        private readonly IProductRepository _productRepository;
        public InventoryColorService(IInventoryColorRepository inventoryColorRepository,
                                     ILogger<IInventoryColorRepository> logger,
                                     IInventoryRepository inventoryRepository,
                                     IColorService colorService,
                                     IColorRepository colorRepository,
                                     IProductRepository productRepository,
                                     IConfiguration configuration)
        {
            this._repository = inventoryColorRepository;
            this._inventoryRepository = inventoryRepository;
            this._logger = logger;
            this._colorService = colorService;
            _productRepository = productRepository;
            _colorRepository = colorRepository;
            this.configuration = configuration;
        }

        private  IConfiguration configuration { get; }
        public ServiceResult Add(InventoryColorDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {

                //validaciones
                dtoAdd.IsValid(this.configuration, this._inventoryRepository, this._colorRepository);
                //codigo para agregar
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
                //validaciones
                dtoAdd.IsValid(this.configuration, this._inventoryRepository, this._colorRepository);
                //codigo para agregar
                InventoryColor inventoryColor = new InventoryColor
                {
                    FK_INVENTORY = dtoAdd.fk_inventory,
                    FK_COLOR_PRIMARY = dtoAdd.fk_color_primary,
                    FK_COLOR_SECONDARY = dtoAdd.fk_color_secondary == 0 ? null : dtoAdd.fk_color_secondary,
                    QUANTITY = dtoAdd.quantity,
                    CREATED_AT = dtoAdd.Date,
                    USER_CREATED = dtoAdd.User
                };
                int id = this._repository.Save(inventoryColor);

                //actualizando el inventario
                Inventory inventory = this._inventoryRepository.GetEntity(dtoAdd.fk_inventory);
                inventory.QUANTITY = this.GetQuantityTotalById(dtoAdd.fk_inventory).Data ;
                this._inventoryRepository.Update(inventory);
                //actualizando el producto
                this._productRepository.UpdateLastReplenishment(inventory.FK_PRODUCT);
                //enviando el mensaje
                result.Message = "Insertado con exito";
                result.Data = id;
            }catch(Exception ex )
            {
                result.Success = false;
                result.Message = $"Error al agregar {ex.Message}";
            }
            return result;
        }

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener el detalle de inventario {ex.Message}";
                throw;
            }
            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
              

                //result.Data = inventoryDetail;
                result.Message = "Obtenidos correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener el detalle de inventario {ex.Message}";
                throw;
            }
            return result;
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
                                                                       InventoryColorId = data.ID,
                                                                       colorPrimary = this._colorService.GetById(data.FK_COLOR_PRIMARY).Data,
                                                                       colorSecondary = (data.FK_COLOR_SECONDARY.HasValue) ? this._colorService.GetById(data.FK_COLOR_SECONDARY.Value).Data : 0,
                                                                       quantity = data.QUANTITY
                                                                   });
                var group = new 
                {
                    FkInventory = id,
                    Inventory = colors
                };
                result.Data = group;
                result.Message = "Obtenidos con exito";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener por el ID de inventario {ex}";
            }
            return result;
        }

        public int GetIdInventory(int inventoryColorId)
        {
            InventoryColor inventoryColor = this._repository.GetEntity(inventoryColorId);
            return inventoryColor.FK_INVENTORY;
        }

        public ServiceResult GetQuantityTotalById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var quantity = this._repository.GetEntities().Where(data => !data.REMOVED).GroupBy(d=> d.FK_INVENTORY).Select(data => new
                                                           {
                                                            data.Key,   
                                                             total = data.Sum(n => n.QUANTITY)
                                                            }).Where(d=> d.Key == id);
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
                dtoRemove.IsValidToRemove(this.configuration, this._repository);

                InventoryColor inventoryToRemove = new InventoryColor
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User,
                    MODIFIED_AT = dtoRemove.Date
                };
                this._repository.Remove(inventoryToRemove);

                //actualizando inventario
                InventoryColor invColor = this._repository.GetEntity(dtoRemove.Id);
                Inventory inventory = this._inventoryRepository.GetEntity(invColor.FK_INVENTORY);
                int newQuantity = this.GetQuantityTotalById(invColor.FK_INVENTORY).Data;
                inventory.QUANTITY = newQuantity;
                this._inventoryRepository.Update(inventory);
                result.Message = "Removido con exito";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar removerlo {ex.Message}.";
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
                    USER_MOD = dtoUpdate.User,
                    QUANTITY = dtoUpdate.quantity,
                    FK_INVENTORY = dtoUpdate.fk_inventory
                };
                //actualizando inventory color
                this._repository.Update(inventoryToUpdate);

                //actualizando inventario
                Inventory inventory = this._inventoryRepository.GetEntity(dtoUpdate.fk_inventory);
                int newQuantity = this.GetQuantityTotalById(dtoUpdate.fk_inventory).Data;
                inventory.QUANTITY = newQuantity;
                this._inventoryRepository.Update(inventory);

                //actualizando producto en caso de que la cantidad aumente
                if (newQuantity > inventory.QUANTITY)
                {
                    this._productRepository.UpdateLastReplenishment(inventory.FK_PRODUCT);
                }

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
