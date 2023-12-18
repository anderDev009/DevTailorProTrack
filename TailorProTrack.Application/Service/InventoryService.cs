using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Inventory;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Application.Service
{
    public class InventoryService : IInventoryService
    {
        //repositories
        private readonly IInventoryRepository _repository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInventorySizeRepository _inventorySizeRepository;
        private readonly ILogger logger;
        //services
        private readonly ISizeService _sizeService;
        public InventoryService(IInventoryRepository inventoryRepository,
                                IConfiguration configuration,
                                ILogger<IInventoryRepository> logger,
                                IInventorySizeRepository inventorySizeRepository,
                                ISizeRepository sizeRepository,
                                IProductRepository productRepository,
                                ISizeService sizeService)
        {
            this._sizeRepository = sizeRepository;
            this._inventorySizeRepository = inventorySizeRepository;
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
                Inventory inventoryToAdd = new Inventory()
                {
                    ID = dtoAdd.Id,
                    LAST_REPLENISHMENT = dtoAdd.last_replenishment
                };
                result.Message = "Producto registrado correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error {ex}";
            }

            return result;
        }

        public ServiceResult GetAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var inventory = this._repository.GetEntities()
              .Join
              (
               this._inventorySizeRepository.GetEntities().Where(data => data.QUANTITY != 0),
               inventory => inventory.ID,
               inventorySize => inventorySize.FK_INVENTORY,
               (inventory,inventorySize) => new {inventory, inventorySize}
              ) 
              .Join
              (
               this._productRepository.GetEntities(),
               combined => combined.inventory.ID,
               product => product.ID,
               (combined,product) => new {combined.inventory,combined.inventorySize,product}
              )
              .Join
              (
              this._sizeRepository.GetEntities(),
              combined => combined.inventorySize.FK_SIZE,
              size => size.ID,
              (combined,size) => new { combined.inventory,combined.inventorySize,combined.product,size}
              )
               .Where(data => !data.inventory.REMOVED)
               .GroupBy(data => new { data.inventory.ID, data.product.NAME_PRODUCT,data.product.SALE_PRICE })
               .Select(group => new InventoryDtoGet
               {
                   id = group.Key.ID,
                   product_name = group.Key.NAME_PRODUCT,
                   price = group.Key.SALE_PRICE,
                   last_replenishment = group.Max(s => s.inventory.LAST_REPLENISHMENT),
                   quantity = group.Sum(g => g.inventorySize.QUANTITY),
                   availableSizes = string.Join(", ", group.Select(g => g.size?.SIZE).Distinct())
               });

                result.Message = "Inventario obtenido correctamente";
                result.Data = inventory;
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
                 combined => combined.inventorySize.FK_SIZE,
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
                      last_replenishment = group.Max(s => s.inventory.LAST_REPLENISHMENT),
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
