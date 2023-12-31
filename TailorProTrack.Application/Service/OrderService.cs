

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Order;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger logger;
        //servicios
        //servicio de producto
        private readonly IProductService _productService;
        //servicio de detalle ordenes
        private readonly IOrderProductService _orderProductService;
        public OrderService(IOrderRepository repository
                            ,ILogger<IOrderRepository> logger
                            ,IConfiguration configuration,
                            IProductService productService
                            ,IOrderProductService orderProductService
                            )
        {
            this._repository = repository;
            this.logger = logger;
            this.Configuration = configuration;
            this._productService = productService;
            this._orderProductService = orderProductService;
        }

        public IConfiguration Configuration { get; }
        //optimizar 
        public ServiceResult Add(OrderDtoAdd dtoAdd)
        {
            ServiceResult result =  new ServiceResult();
            try 
            {
                //logica para agregarele la cantidad
                decimal amount = 0;
                foreach(var item in dtoAdd.products)
                {
                    amount += this._productService.GetPrice(item.FkProduct) * item.Quantity;
                    
                };

                //agregando la orden
                Order orderToAdd = new Order
                {
                    CHECKED = dtoAdd.Checked,
                    FK_CLIENT = dtoAdd.FkClient,
                    FK_USER = dtoAdd.FkUser,
                    AMOUNT = amount
                };
                int idOrder = this._repository.Save(orderToAdd);
                //agregando el detalle de la orden
                var resultOrderProd = this._orderProductService.AddMany(dtoAdd.products,idOrder);
                if (!resultOrderProd.Success)
                {
                    return resultOrderProd;
                }
                //mensaje de exito
                result.Message = "Orden registrada con exito.";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al registrar la orden: {ex.Message}";
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

        public ServiceResult Remove(OrderDtoRemove dtoRemove)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Update(OrderDtoUpdate dtoUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
