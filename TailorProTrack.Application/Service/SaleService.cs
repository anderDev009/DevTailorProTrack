
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Sale;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class SaleService : ISaleService
    {
        private readonly ISalesRepository _repository;
        private readonly ILogger logger;
        //repositorio de ordenes
        private readonly IOrderRepository _orderRepository;
        public SaleService(ISalesRepository saleRepository, 
                           ILogger<ISalesRepository> logger,
                           IOrderRepository orderRepository) 
        { 
            _repository = saleRepository;
            this.logger = logger;
            _orderRepository = orderRepository;
        }
        public ServiceResult Add(SaleDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //obteniendo el total 
                decimal amount = this._orderRepository.GetEntities().Where(d => d.ID == dtoAdd.FkOrder).Select(d=>d.AMOUNT).First();
                //----------------
                Sales sale = new Sales
                {
                    COD_ISC = dtoAdd.CodIsc,
                    FK_ORDER = dtoAdd.FkOrder,
                    USER_CREATED = dtoAdd.User,
                    TOTAL_AMOUNT = amount
                };

                
                int id = this._repository.Save(sale);
                result.Data = id;
                result.Message = "Agregado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar la venta {ex.Message}.";
            }

            return result;
        }

        public ServiceResult GetAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var sales = this._repository.GetEntities().Where(d => !d.REMOVED).Select(d => new SaleDtoGet
                {
                    Id = d.ID,
                    CodIsc = d.COD_ISC,
                    FkOrder = d.FK_ORDER,
                    Amount = d.TOTAL_AMOUNT
                });

                result.Data = sales;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener las ventas {ex.Message}.";
            }

            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var sale = this._repository.GetEntities().Where(d => d.ID == id).Select(d => new {d.ID,d.FK_ORDER,d.TOTAL_AMOUNT,d.COD_ISC}).First();
                var order = this._orderRepository.GetEntity(sale.FK_ORDER);
                var saleOrder = new
                {
                    Sale = sale,
                    Order = order,
                };

                result.Data = saleOrder;
                result.Message = "Obtenido con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar la venta {ex.Message}.";
            }

            return result;
        }

        public ServiceResult Remove(SaleDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Sales sale = new Sales
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User
                };

                this._repository.Remove(sale);
                result.Message = "Removido con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar remover la venta {ex.Message}.";
            }

            return result;
        }

        public ServiceResult Update(SaleDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try
            {

                decimal amount = this._orderRepository.GetEntities().Where(d => d.ID == dtoUpdate.FkOrder).Select(d => d.AMOUNT).First();
                Sales sale = new Sales
                {
                    COD_ISC = dtoUpdate.CodIsc,
                    FK_ORDER = dtoUpdate.FkOrder,
                    TOTAL_AMOUNT = amount,
                    USER_MOD = dtoUpdate.User
                };

                this._repository.Update(sale);
                result.Message = "Actualizado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar la venta {ex.Message}.";
            }

            return result;
        }
    }
}
