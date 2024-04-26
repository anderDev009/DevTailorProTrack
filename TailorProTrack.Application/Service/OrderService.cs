

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.InventoryColor;
using TailorProTrack.Application.Dtos.Order;
using TailorProTrack.Application.Dtos.PreOrder;
using TailorProTrack.Application.Extentions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger logger;
        private readonly IMapper _mapper;
        //repositorios
        //user repository
        private readonly IUserRepository _userRepository;
        //color repository
        private readonly IColorRepository _colorRepository;
        //inventory color repository 
        private readonly IInventoryColorRepository _inventoryColorRepository;
        //size repository
        private readonly ISizeRepository _sizeRepository;
        //inventario
        private readonly IInventoryRepository _inventoryRepository;
        //types
        private readonly ITypeProdRepository _repositoryType;
        //product
        private readonly IProductRepository _productRepository;
        //orderProduct
        private readonly IOrderProductRepository _orderProductRepository;
        //preOrderProducts
        private readonly IPreOrderProductsRepository _preOrderProductsRepository;
        //cliente
        private readonly IClientRepository _clientRepository;
        //preOrder 
        private readonly IPreOrderRepository _preOrderRepository;
        //servicios
        private readonly IInventoryColorService _inventoryColorService;
        //servicio de inventario
        private readonly IInventoryService _inventoryService;
        //servicio de producto
        private readonly IProductService _productService;
        //servicio de detalle ordenes
        private readonly IOrderProductService _orderProductService;

        public OrderService(IOrderRepository repository
                            , ILogger<IOrderRepository> logger
                            , IConfiguration configuration,
                            IProductService productService
                            , IOrderProductService orderProductService
                            , IClientRepository clientRepository
                            , IOrderProductRepository orderProductRepository
                            , IProductRepository productRepository
                            , ITypeProdRepository typeProdRepository
                            , IInventoryService inventoryService
                            , IInventoryRepository inventoryRepository
                            , ISizeRepository sizeRepository,
                            IInventoryColorRepository inventoryColorRepository,
                            IColorRepository colorRepository,
                            IUserRepository userRepository,
                            IPreOrderRepository preOrderRepository,
                            IPreOrderProductsRepository preOrderProductsRepository,
                            IInventoryColorService inventoryColorService,
                            IMapper mapper
                            )
        {
            this._repository = repository;
            this.logger = logger;
            this.Configuration = configuration;
            this._productService = productService;
            this._orderProductService = orderProductService;
            this._clientRepository = clientRepository;
            this._orderProductRepository = orderProductRepository;
            this._productRepository = productRepository;
            _repositoryType = typeProdRepository;
            this._inventoryService = inventoryService;
            this._inventoryRepository = inventoryRepository;
            this._sizeRepository = sizeRepository;
            this._inventoryColorRepository = inventoryColorRepository;
            this._colorRepository = colorRepository;
            _userRepository = userRepository;
            _preOrderRepository = preOrderRepository;
            _preOrderProductsRepository = preOrderProductsRepository;
            _inventoryColorService = inventoryColorService;
            _mapper = mapper;
        }

        public IConfiguration Configuration { get; }
        //optimizar 
        public ServiceResult Add(OrderDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //validaciones
                string message = dtoAdd.IsValidToAdd(this.Configuration, this._clientRepository, this._userRepository, _preOrderRepository
                    , _preOrderProductsRepository, _inventoryRepository, _inventoryColorRepository, _orderProductRepository);
                //logica para agregarele la cantidad
                decimal amount = 0;
                foreach (var item in dtoAdd.products)
                {
                    amount += this._inventoryService.GetPriceProductByInventoryId(item.FkInventoryColor) * item.Quantity;

                };

                //agregando la orden
                Order orderToAdd = new Order
                {
                    CHECKED = dtoAdd.Checked,
                    FK_CLIENT = dtoAdd.FkClient,
                    FK_USER = dtoAdd.FkUser,
                    FK_PREORDER = dtoAdd.FkPreOrder,
                    AMOUNT = amount,
                    DESCRIPTION_JOB = dtoAdd.DescriptionJob,
                    STATUS_ORDER = "pendiente",
                    SEND_TO = dtoAdd.sendTo
                };
                int idOrder = this._repository.Save(orderToAdd);
                //agregando el detalle de la orden
                var resultOrderProd = this._orderProductService.AddMany(dtoAdd.products, idOrder);
                if (!resultOrderProd.Success)
                {
                    return resultOrderProd;
                }
                //mensaje de exito
                result.Message = "Orden registrada con exito.";
                //la validacion devuelve una cadena donde indica el ID de los productos que no se pudieron registrar
                if(message != "")
                {
                    result.Message += $" Productos no registrados por falta de cantidad: {message}";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al registrar la orden: {ex.Message}";
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

                var ordersMapped = _repository.SearchEntities()
                    .Include(d => d.Client)
                    .Include(d => d.OrderProducts)
                    //.ThenInclude(d => d.InventoryColor)
                     .Skip((@params.Page - 1) * @params.ItemsPerPage)
                                             .Take(@params.ItemsPerPage)
                                             .ToList();
                //var orders = this._repository.GetEntitiesPaginated(@params.Page, @params.ItemsPerPage).Where(d => !d.REMOVED)
                //                             .Join
                //                             (
                //                                this._clientRepository.GetEntities().Select(data => new
                //                                {
                //                                    data.ID,
                //                                    data.FIRST_NAME,
                //                                    data.FIRST_SURNAME,
                //                                    data.LAST_SURNAME
                //                                }),
                //                                orderFK => orderFK.FK_CLIENT,
                //                                client => client.ID,
                //                                (orderFK, client) => new { orderFK, client }
                //                             )
                //                             .OrderBy(data => data.orderFK.ID)
                //                             .Select(data => new OrderDtoGet
                //                             {
                //                                 Id = data.orderFK.ID,
                //                                 FullName = $"{data.client.FIRST_NAME} {data.client.FIRST_SURNAME} {data.client.LAST_SURNAME}",
                //                                 Amount = data.orderFK.AMOUNT,
                //                                 Checked = data.orderFK.CHECKED,
                //                                 DescriptionJob = data.orderFK.DESCRIPTION_JOB,
                //                                 StatusOrder = data.orderFK.STATUS_ORDER,
                //                                 Quantity = this._orderProductService.GetQuantityByOrderId(data.orderFK.ID).Data,
                //                             })
                //                             .Skip((@params.Page - 1) * @params.ItemsPerPage)
                //                             .Take(@params.ItemsPerPage)
                //                             .ToList();
                result.Data = _mapper.Map<List<OrderDtoGetMapped>>(ordersMapped);
                result.Header = header;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener todos: {ex.Message}";
            }
            return result;

        }

        public ServiceResult GetAmountTotalById(int Id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var order = this._repository.GetEntities().Where(d => d.ID == Id).Select(d => new { d.ID, d.AMOUNT });

                result.Data = order;
                result.Message = "Obtenido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener el precio total {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var order = this._repository.GetEntities()
                                                .Where(d => d.ID == id)
                                                .Join
                                                (
                                                   this._clientRepository.GetEntities().Select(data => new
                                                   {
                                                       data.ID,
                                                       data.FIRST_NAME,
                                                       data.FIRST_SURNAME,
                                                       data.LAST_SURNAME
                                                   }),
                                                   orderFK => orderFK.FK_CLIENT,
                                                   client => client.ID,
                                                   (orderFK, client) => new { orderFK, client }
                                                )
                                                .Select(data => new OrderDtoGetDetail
                                                {
                                                    Id = data.orderFK.ID,
                                                    FullName = $"{data.client.FIRST_NAME} {data.client.FIRST_SURNAME} {data.client.LAST_SURNAME}",
                                                    Amount = data.orderFK.AMOUNT,
                                                    Checked = data.orderFK.CHECKED,
                                                    Quantity = this._orderProductService.GetQuantityByOrderId(data.orderFK.ID).Data[0],
                                                    Products = this._orderProductRepository.GetEntities().Where(data => data.FK_ORDER == id)
                                                                                 .Join
                                                                                 (
                                                                                  this._inventoryColorRepository.GetEntities().Select(d => new { d.ID, d.FK_INVENTORY, d.FK_COLOR_PRIMARY, d.FK_COLOR_SECONDARY }),
                                                                                  order => order.FK_INVENTORYCOLOR,
                                                                                  inventoryColor => inventoryColor.ID,
                                                                                  (order, inventoryColor) => new { order, inventoryColor }
                                                                                 )//color primario
                                                                                 .Join
                                                                                 (
                                                                                 this._colorRepository.GetEntities().Select(d => new { d.ID, d.CODE_COLOR, d.COLORNAME }),
                                                                                 combined => combined.inventoryColor.FK_COLOR_PRIMARY,
                                                                                 colorPrimary => colorPrimary.ID,
                                                                                 (combined, colorPrimary) => new { combined.order, combined.inventoryColor, colorPrimary }
                                                                                 )//colorsecundario
                                                                                .Join
                                                                                 (
                                                                                 this._colorRepository.GetEntities().Select(d => new { d.ID, d.CODE_COLOR, d.COLORNAME }),
                                                                                 combined => combined.inventoryColor.FK_COLOR_SECONDARY,
                                                                                 colorSecondary => colorSecondary.ID,
                                                                                 (combined, colorSecondary) => new { combined.order, combined.inventoryColor, combined.colorPrimary, colorSecondary }
                                                                                 )
                                                                                 .Join
                                                                                 (
                                                                                 this._inventoryRepository.GetEntities().Select(d => new { d.ID, d.FK_PRODUCT, d.FK_SIZE }),
                                                                                 combined => combined.inventoryColor.FK_INVENTORY,
                                                                                 inventory => inventory.ID,
                                                                                 (combined, inventory) => new { combined.colorPrimary, combined.colorSecondary, combined.order, combined.inventoryColor, inventory }
                                                                                 )
                                                                                 .Join
                                                                                 (
                                                                                 this._sizeRepository.GetEntities().Select(d => new { d.ID, d.SIZE }),
                                                                                 combined => combined.inventory.FK_SIZE,
                                                                                 size => size.ID,
                                                                                 (combined, size) => new { combined.colorPrimary, combined.colorSecondary, combined.inventoryColor, combined.inventory, combined.order, size }
                                                                                 )
                                                                                 .Join(
                                                                                 this._productRepository.GetEntities().Join(this._repositoryType.GetEntities().Select(d => new { d.ID, d.TYPE_PROD })
                                                                                                                         .ToList(), product => product.FK_TYPE, type => type.ID, (product, type) => new { product, type })
                                                                                                                         .Select(d => new { d.product.ID, d.product.NAME_PRODUCT, d.product.SALE_PRICE, d.product.DESCRIPTION_PRODUCT, d.type.TYPE_PROD }),
                                                                                 combined => combined.inventory.FK_PRODUCT,
                                                                                 product => product.ID,
                                                                                 (combined, product) => new { combined, product }
                                                                                 )
                                                                                 .GroupBy(d => d.combined.inventoryColor.ID)
                                                                                 .Select(data => new
                                                                                 {
                                                                                     Id = data.Select(d => d.combined.inventoryColor.ID).First(),
                                                                                     ProductName = data.Select(d => d.product.NAME_PRODUCT).First(),
                                                                                     Price = data.Select(d => d.product.SALE_PRICE).First(),
                                                                                     Quantity = data.Select(d => d.combined.order.QUANTITY).First(),
                                                                                     Size = data.Select(d => d.combined.size.SIZE).First(),
                                                                                     ColorPrimary = data.Select(d => new { d.combined.colorPrimary.COLORNAME, d.combined.colorPrimary.CODE_COLOR }).First(),
                                                                                     ColorSecondary = data.Select(d => new { d.combined.colorSecondary.COLORNAME, d.combined.colorSecondary.CODE_COLOR }).First()
                                                                                 })
                                                });
                if (order == null) throw new Exception("No existe");
                result.Data = order;
                result.Message = "Obtenido con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener la orden: {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetInvColorAvailableToAddOrder(List<PreOrderDtoFkSizeFkProduct> keys)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                List<InventoryColorDtoGetWithId> invColors = new List<InventoryColorDtoGetWithId>();
                foreach (var key in keys)
                {
                    var invColor = _inventoryColorService.SearchAvailabilityToAddOrder(key.FkSize,key.FkProduct, key.FkColorPrimary, key.FkColorSecondary);
                    if (invColor.InventoryColorId != 0)
                    {
                        invColors.Add(invColor);
                    }
                }
                result.Data = invColors;
                result.Message = "Obtenidos con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
                throw;
            }
            return result;
        }

        public ServiceResult GetOrder(int Id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var orders = this._repository.GetEntities().Where(d => !d.REMOVED && d.ID == Id)
                                             .Join
                                             (
                                                this._clientRepository.GetEntities().Select(data => new
                                                {
                                                    data.ID,
                                                    data.FIRST_NAME,
                                                    data.FIRST_SURNAME,
                                                    data.LAST_SURNAME
                                                }),
                                                orderFK => orderFK.FK_CLIENT,
                                                client => client.ID,
                                                (orderFK, client) => new { orderFK, client }
                                             )
                                             .Select(data => new OrderDtoGetFull
                                             {
                                                 Id = data.orderFK.ID,
                                                 FullName = $"{data.client.FIRST_NAME} {data.client.FIRST_SURNAME} {data.client.LAST_SURNAME}",
                                                 Amount = data.orderFK.AMOUNT,
                                                 Checked = data.orderFK.CHECKED,
                                                 DescriptionJob = data.orderFK.DESCRIPTION_JOB,
                                                 StatusOrder = data.orderFK.STATUS_ORDER,
                                                 Quantity = this._orderProductService.GetQuantityByOrderId(data.orderFK.ID).Data,
                                                 Detail = this.GetById(Id).Data
                                             });
                result.Data = orders;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener todos: {ex.Message}";
            }
            return result;
        }



        public ServiceResultWithHeader GetOrderJobs(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {
                int registerCount = this._repository.GetEntities().Where(d => d.CHECKED).Count();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);

                var orders = this._repository.GetEntities().Where(d => !d.REMOVED && d.CHECKED)
                                         .Join
                                         (
                                            this._clientRepository.GetEntities().Select(data => new
                                            {
                                                data.ID,
                                                data.FIRST_NAME,
                                                data.FIRST_SURNAME,
                                                data.LAST_SURNAME
                                            }),
                                            orderFK => orderFK.FK_CLIENT,
                                            client => client.ID,
                                            (orderFK, client) => new { orderFK, client }
                                         )
                                         .OrderBy(data => data.orderFK.ID)
                                         .Select(data => new OrderDtoGet
                                         {
                                             Id = data.orderFK.ID,
                                             FullName = $"{data.client.FIRST_NAME} {data.client.FIRST_SURNAME} {data.client.LAST_SURNAME}",
                                             Amount = data.orderFK.AMOUNT,
                                             Checked = data.orderFK.CHECKED,
                                             DescriptionJob = data.orderFK.DESCRIPTION_JOB,
                                             StatusOrder = data.orderFK.STATUS_ORDER,
                                             Quantity = this._orderProductService.GetQuantityByOrderId(data.orderFK.ID).Data,
                                         })
                                         .Skip((@params.Page - 1) * @params.ItemsPerPage)
                                         .Take(@params.ItemsPerPage)
                                         .ToList();

                result.Data = orders;
                result.Header = header;
                result.Message = "Obtenidos con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener las ordenes de trabajo {ex.Message}.";
            }
            return result;
        }

        public ServiceResult Remove(OrderDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Order order = new Order
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User,
                };
                this._repository.Remove(order);

                result.Message = "Removido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar remover {ex.Message}";
            }

            return result;
        }

        public ServiceResult Update(OrderDtoUpdate dtoUpdate)
        {
            throw new NotImplementedException();
        }

        public ServiceResult UpdateAmount(int Id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var amount = this._orderProductService.GetQuantityByOrderId(Id);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar el monto {ex.Message}";
            }
            return result;
        }

        public ServiceResult UpdateStatusOrder(OrderDtoUpdateStatus dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Order order = new Order
                {
                    ID = dtoUpdate.Id,
                    USER_MOD = dtoUpdate.User,
                    STATUS_ORDER = dtoUpdate.StatusOrder,
                    CHECKED = dtoUpdate.Checked
                };
                this._repository.UpdateStatusOrder(order);
                result.Message = "Actualizado correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar la orden {ex.Message}";
            }
            return result;
        }
    }
}
