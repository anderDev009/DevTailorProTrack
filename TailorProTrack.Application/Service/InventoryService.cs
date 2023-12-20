using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.Application.Dtos.Inventory;
using TailorProTrack.Application.Dtos.InventoryColor;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Application.Service
{
    public class InventoryService : IInventoryService
    {
        //repositories
        private readonly IInventoryColorService _inventoryColorService;
        private readonly IInventoryRepository _repository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInventoryColorRepository _inventorySizeRepository;
        private readonly ILogger logger;
        //services
        private readonly ISizeService _sizeService;
        public InventoryService(IInventoryRepository inventoryRepository,
                                IConfiguration configuration,
                                ILogger<IInventoryRepository> logger,
                                IInventoryColorRepository inventorySizeRepository,
                                ISizeRepository sizeRepository,
                                IProductRepository productRepository,
                                IInventoryColorService inventoryColorService,
                                ISizeService sizeService)
        {
            this._sizeRepository = sizeRepository;
            this._inventorySizeRepository = inventorySizeRepository;
            //inventory color service
            this._inventoryColorService = inventoryColorService;
            this.Configuration = configuration;
            this._repository = inventoryRepository;
            this.logger = logger;
            _productRepository = productRepository;
            _sizeService = sizeService;
        }

        private IConfiguration Configuration { get; }

        public ServiceResult Add(InventoryDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {


                Inventory inventory = new Inventory
                {
                    ID = 0,
                    FK_PRODUCT = dtoAdd.fk_product,
                    QUANTITY = dtoAdd.inventoryColors.Sum(color => color.quantity),
                    FK_SIZE = dtoAdd.fk_size,
                    USER_CREATED = dtoAdd.User,
                    CREATED_AT = dtoAdd.Date
                };
                int idInventory = this._repository.Save(inventory);
                //agregando los colores al inventario
                dtoAdd.inventoryColors.ForEach(color =>
                {
                    color.fk_inventory = idInventory;
                    this._inventoryColorService.Add(color);
                    result.Data = color;
                });
                result.Message = "Registrado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error el registrar el inventario {ex}";
            }

            return result;
        }

        public ServiceResult GetAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var inventory = this._repository.GetEntities()
                                 .Select(data => new
                                 {
                                     data.ID,
                                     data.FK_SIZE,
                                     data.FK_PRODUCT,
                                     data.QUANTITY,
                                     data.REMOVED
                                 })
                                 .Join
                                  (
                                  this._sizeRepository.GetEntities()
                                                      .Select(data => new
                                                      {
                                                          data.ID,
                                                          data.SIZE
                                                      }
                                                       ),
                                 inventory => inventory.FK_SIZE,
                                 size => size.ID,
                                 (inventory, size) => new { inventory, size }
                                  )
                                 .Join
                                 (
                                 this._productRepository.GetEntities()
                                                        .Select(data => new
                                                        {
                                                            data.ID,
                                                            data.NAME_PRODUCT,
                                                            data.DESCRIPTION_PRODUCT,
                                                            data.SALE_PRICE,
                                                            data.LAST_REPLENISHMENT
                                                        }),
                                 combined => combined.inventory.FK_PRODUCT,
                                 product => product.ID,
                                 (combined, product) => new { combined.inventory, combined.size, product }
                                 )
                                 .Where(data => !data.inventory.REMOVED)
                                 .GroupBy(data => new { data.product.ID, data.product.NAME_PRODUCT, data.product.SALE_PRICE})
                                 .Select(group => new InventoryDtoGet
                                 {
                                     id = group.Key.ID,
                                     product_name = group.Key.NAME_PRODUCT,
                                     price = group.Key.SALE_PRICE,
                                     quantity = group.Sum(d => d.inventory.QUANTITY)
                                 }); 
                result.Data = inventory;
                result.Message = "Inventario obtenido correctamente";
                //result.Data = inventory;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error {ex}";
            }

            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var sizesById = this._sizeService.GetSizesAvailablesProductById(id);

                var inventory = this._repository.GetEntities()
                 .Join
                 (
                  this._inventorySizeRepository.GetEntities().Where(data => data.QUANTITY != 0),
                  inventory => inventory.ID,
                  inventorySize => inventorySize.FK_INVENTORY,
                  (inventory, inventorySize) => new { inventory, inventorySize }
                 )
                 .Join
                 (
                  this._productRepository.GetEntities(),
                  combined => combined.inventory.ID,
                  product => product.ID,
                  (combined, product) => new { combined.inventory, combined.inventorySize, product }
                 )
                 .Join
                 (
                 this._sizeRepository.GetEntities(),
                 combined => combined.inventorySize.FK_INVENTORY,
                 size => size.ID,
                 (combined, size) => new { combined.inventory, combined.inventorySize, combined.product, size }
                 )
                  .Where(data => !data.inventory.REMOVED)
                  .GroupBy(data => new { data.inventory.ID, data.product.NAME_PRODUCT, data.product.SALE_PRICE })
                  .Select(group => new 
                  {
                      id = group.Key.ID,
                      product_name = group.Key.NAME_PRODUCT,
                      price = group.Key.SALE_PRICE,
                      //last_replenishment = group.Max(s => s.inventory.LAST_REPLENISHMENT),
                      quantity = group.Sum(g => g.inventorySize.QUANTITY),
                      availableSizes = sizesById.Data
                  });


                result.Data = inventory;
                result.Message = "Obtenido correctamente";
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener el producto ${ex}";
            }
          
            return result;
        }

        public ServiceResult Remove(InventoryDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Inventory inventory = new Inventory()
                {
                    USER_MOD = dtoRemove.User,
                    REMOVED = true,
                    MODIFIED_AT = dtoRemove.Date
                };
                this._repository.Remove(inventory);
                result.Message = "Removido con exito";
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar eliminar ${ex}";
            }
            return result;
        }

        public ServiceResult Update(InventoryDtoUpdate dtoUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
