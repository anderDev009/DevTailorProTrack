using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Contracts.Size;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Inventory;
using TailorProTrack.Application.Extentions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

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
        private readonly IProductService _productService;
        private readonly ISizeService _sizeService;
        public InventoryService(IInventoryRepository inventoryRepository,
                                IConfiguration configuration,
                                ILogger<IInventoryRepository> logger,
                                IInventoryColorRepository inventorySizeRepository,
                                ISizeRepository sizeRepository,
                                IProductRepository productRepository,
                                IInventoryColorService inventoryColorService,
                                ISizeService sizeService,
                                IProductService productService)
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
            //service product
            _productService = productService;
        }

        private IConfiguration Configuration { get; }

        public ServiceResult Add(InventoryDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //validaciones
                dtoAdd.IsValid(this.Configuration, this._productRepository, this._sizeRepository);
                //codigo para agregar
                Inventory inventory = new Inventory
                {
                    ID = 0,
                    FK_PRODUCT = dtoAdd.fk_product,
                    QUANTITY = dtoAdd.inventoryColors.Sum(color => color.quantity),
                    FK_SIZE = dtoAdd.fk_size,
                    USER_CREATED = dtoAdd.User,
                };
                int idInventory = this._repository.Save(inventory);
                //agregando los colores al inventario
                dtoAdd.inventoryColors.ForEach(color =>
                {
                    color.fk_inventory = idInventory;
                    this._inventoryColorService.Add(color);
                });
                //actualizando la ultima actualizacion del producto
                this._productRepository.UpdateLastReplenishment(dtoAdd.fk_product);
                result.Message = "Registrado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error el registrar el inventario {ex}";
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

                var inventory = this._repository.GetEntitiesPaginated(@params.Page, @params.ItemsPerPage)
                                 .Where(d => !d.REMOVED)
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
                                                      .Where(d => !d.REMOVED) 
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
                                                            data.LAST_REPLENISHMENT,
                                                        }),
                                 combined => combined.inventory.FK_PRODUCT,
                                 product => product.ID,
                                 (combined, product) => new { combined.inventory, combined.size, product }
                                 )
                                 .Where(data => !data.inventory.REMOVED)
                                 .GroupBy(data => new { data.product.ID, data.product.NAME_PRODUCT, data.product.SALE_PRICE, data.product.LAST_REPLENISHMENT })
                                 .Select(group => new InventoryDtoGet
                                 {
                                     id = group.Key.ID,
                                     product_name = group.Key.NAME_PRODUCT,
                                     price = group.Key.SALE_PRICE,
                                     quantity = group.Sum(d => d.inventory.QUANTITY),
                                     availableSizes = this._sizeService.GetSizesAvailablesProductById(group.Key.ID).Data,
                                     last_replenishment = (group.Key.LAST_REPLENISHMENT.ToString("MM/dd/yyyy") == "01/01/0001" ? "" : group.Key.LAST_REPLENISHMENT.ToString("MM/dd/yyyy"))
                                 })
                                 .ToList(); 
                result.Data = inventory;
                result.Header = header;
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
                var sizesById = this._sizeService.GetSizesAvailablesProductById(id).Data;
                //validar si sizesById no viene nulo

                
                var product = this._productService.GetById(id).Data;

                List<dynamic> colors = new List<dynamic>();
                foreach (var item in sizesById)
                {
                    var inventoryColor = this._inventoryColorService.GetByIdInventory(item.idInventory).Data;// sizesById.Data.idInventory;
                    colors.Add(inventoryColor);
                    //Console.WriteLine(item);
                }
                var inventory = this._repository.GetEntities()
                                                .Where(data => 
                                                data.FK_PRODUCT == id && !data.REMOVED && data.QUANTITY != 0)
                                                .GroupBy(data=> new { data.FK_PRODUCT})
                                                .Select(data => new 
                                                {
                                                    product,
                                                    quantity = data.Sum(d=> d.QUANTITY),
                                                    inventory = new
                                                    {
                                                         sizes = sizesById,
                                                         colors
                                                                                                   }
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

        public ServiceResult GetInventoryById(int inventoryId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var inventory = this._repository.GetEntities().Where(d => !d.REMOVED && d.ID == inventoryId)
                                                .Join
                                                (
                                                    this._productRepository.GetEntities().Select(d => new { d.ID, d.NAME_PRODUCT }),
                                                    inventory => inventory.FK_PRODUCT,
                                                    product => product.ID,
                                                    (inventory, product) => new { inventory, product }
                                                )
                                                .Join
                                                (
                                                    this._sizeRepository.GetEntities().Select(d => new { d.ID, d.SIZE }),
                                                    combined => combined.inventory.FK_SIZE,
                                                    size => size.ID,
                                                    (combined, size) => new { combined.inventory, combined.product, size }
                                                )
                                                .Select(data => new
                                                {
                                                    IdInventory = data.inventory.ID,
                                                    IdProduct = data.product.ID,
                                                    NameProduct = data.product.NAME_PRODUCT,
                                                    Size = data.size.SIZE,
                                                    //no actualizable directamente
                                                    Quantity = data.inventory.QUANTITY
                                                });

                result.Data = inventory;
                result.Message = "Inventario obtenido correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error obteniendo el inventario: {ex.Message}";
            }
            return result;
        }

        public decimal GetPriceProductByInventoryId(int inventoryId)
        {
            int id = this._inventoryColorService.GetIdInventory(inventoryId);
            Inventory inventory = this._repository.GetEntity(id);
            return this._productService.GetPrice(inventory.FK_PRODUCT);
        }

        public ServiceResult Remove(InventoryDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Inventory inv = new Inventory
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User,
                };
                this._repository.Remove(inv);
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
            ServiceResult result = new ServiceResult();
            try
            {
                Inventory inventory = new Inventory
                {
                    ID = dtoUpdate.Id,
                    FK_PRODUCT = dtoUpdate.fk_product,
                    FK_SIZE = dtoUpdate.fk_size,
                    MODIFIED_AT = dtoUpdate.Date,
                    USER_MOD = dtoUpdate.User
                };
                this._repository.Update(inventory);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar: {ex.Message}";
            }
            return result;
        }
    }
}
