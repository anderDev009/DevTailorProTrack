
using Microsoft.Extensions.Logging;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.OrderProduct;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class OrderProductService : IOrderProductService
    {
        private readonly IOrderProductRepository _repository;
        private readonly ILogger logger;
        
        public OrderProductService(IOrderProductRepository repository, ILogger<IProductRepository> logger)
        {
            this._repository = repository;
            this.logger = logger;   
        }

        public ServiceResult Add(OrderProductDtoAdd dtoAdd)
        {
            throw new NotImplementedException();
        }

        public ServiceResult AddMany(List<OrderProductDtoAdd> products, int idOrder)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                List<OrderProduct> productsToAdd = new List<OrderProduct>();    
                foreach(var item in products)
                {
                    productsToAdd.Add(new OrderProduct
                    {
                        FK_ORDER = idOrder,
                        FK_PRODUCT = item.FkProduct,
                        QUANTITY =  item.Quantity
                    });
                }
                this._repository.SaveMany(productsToAdd);
                result.Message = "Agregados con exito";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al guardar los productos: {ex.Message}";
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

        public ServiceResult Remove(OrderProductDtoRemove dtoRemove)
        {
            throw new NotImplementedException();
        }



        public ServiceResult Update(OrderProductDtoUpdate dtoUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
