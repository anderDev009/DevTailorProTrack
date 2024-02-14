
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Sale;
using TailorProTrack.Application.Extentions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class SaleService : ISaleService
    {
        private readonly ISalesRepository _repository;
        private readonly ILogger logger;
        //repositorio de preorder
        private readonly IPreOrderRepository _preOrderRepository;
        //repositorio de productos
        //repositorio de preorder products
        private readonly IPreOrderProductsRepository _preorderProductsRepository;
        //servicio de preorder
        private readonly IPreOrderService _preorderService;
        public SaleService(ISalesRepository saleRepository,
                           ILogger<ISalesRepository> logger,
                           IPreOrderProductsRepository preorderProductsRepository,
                           IPreOrderService preorderService,
                           IConfiguration configuration,
                           IPreOrderRepository preOrderRepository)
        {
            _repository = saleRepository;
            this.logger = logger;
            this.configuration = configuration;
            _preorderProductsRepository = preorderProductsRepository;
            _preorderService = preorderService;
            _preOrderRepository = preOrderRepository;
        }
        public IConfiguration configuration { get; }
        public ServiceResult Add(SaleDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                dtoAdd.IsValid(configuration,_preOrderRepository);

                //obteniendo el total 
                decimal amount = _preorderProductsRepository.GetAmountByIdPreOrder(dtoAdd.FkOrder);
                //----------------
                Sales sale = new Sales
                {
                    COD_ISC = dtoAdd.CodIsc,
                    FK_PREORDER = dtoAdd.FkOrder,
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

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {
                int registerCount = this._repository.GetEntitiesPaginated(@params.Page, @params.ItemsPerPage).Where(d => !d.REMOVED).Count();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);

                var sales = this._repository.GetEntities().Where(d => !d.REMOVED)
                    .OrderBy(d => d.ID)
                    .Select(d => new SaleDtoGet
                    {
                        Id = d.ID,
                        CodIsc = d.COD_ISC,
                        FkOrder = d.FK_PREORDER,
                        Amount = d.TOTAL_AMOUNT
                    }).ToList();

                result.Data = sales;
                result.Header = header;
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
                var sale = this._repository.GetEntities().Where(d => d.ID == id).Select(d => new { d.ID, d.FK_PREORDER, d.TOTAL_AMOUNT, d.COD_ISC }).First();
                var order = _preorderService.GetById(sale.FK_PREORDER);
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
                dtoUpdate.IsValid(configuration,_preOrderRepository);
                decimal amount = _preorderProductsRepository.GetAmountByIdPreOrder(dtoUpdate.FkOrder);
                Sales sale = new Sales
                {
                    COD_ISC = dtoUpdate.CodIsc,
                    FK_PREORDER = dtoUpdate.FkOrder,
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
