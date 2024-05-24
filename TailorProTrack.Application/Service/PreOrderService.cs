using AutoMapper;
using Microsoft.EntityFrameworkCore;

using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Contracts.Client;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.PreOrder;
using TailorProTrack.Application.Dtos.PreOrderProducts;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class PreOrderService : IPreOrderService
    {

        //repositorios
        private readonly IPreOrderRepository _preOrderRepository;

        private readonly IPaymentRepository _paymentRepository;
        //mapper
        private readonly IMapper _mapper;

        //servicios 
        private readonly IPreOrderProductService _preOrderProductService;
        //
        private readonly IClientService _clientService;
        public PreOrderService(IPreOrderRepository preOrderRepository,
                        IPreOrderProductService preOrderProductService,
                        IPaymentRepository paymentRepository ,
                        IClientService clientService, IMapper mapper)
        {
            _preOrderRepository = preOrderRepository;
            _preOrderProductService = preOrderProductService;
            _clientService = clientService;
            _mapper = mapper;
            _paymentRepository = paymentRepository;
        }

        public ServiceResult Add(PreOrderDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                PreOrder preOrder = new PreOrder
                {
                    FK_CLIENT = dtoAdd.FkClient,
                    DATE_DELIVERY = dtoAdd.DateDelivery,
                    COMPLETED = false

                };

                int id = this._preOrderRepository.Save(preOrder);
                var data = this._preOrderProductService.SaveMany(dtoAdd.productsDtoAdds.Select(data => new PreOrderProductsDtoAdd
                {
                    FkColorPrimary = data.FkColorPrimary,
                    FkColorSecondary = data.FkColorSecondary,
                    FkProduct = data.FkProduct,
                    FkSize = data.FkSize,
                    Quantity = data.Quantity,
                    User = dtoAdd.User
                }).ToList(), id);
                result.Message = data.Message;
                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al agregar el pedido: {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetAccountsReceivable()
        {
            ServiceResult result = new();
            try
            {
                var report = _preOrderRepository.GetAccountsReceivable();
                List<PreOrderDtoGetMapped> preOrders = _mapper.Map<List<PreOrderDtoGetMapped>>(report);
                foreach (var item in preOrders)
                {
                    item.Amount = _paymentRepository.GetAmountPendingByIdPreOrder(item.ID);
                }
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }
            return result;
        }

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {
                var countRegister = this._preOrderRepository.CountEntities();
                PaginationMetaData header = new PaginationMetaData(countRegister, @params.Page, @params.ItemsPerPage);

                var preOrders = this._preOrderRepository.SearchEntities()
                                                        .Include(x => x.PreOrderProducts)
                                                           .ThenInclude(x => x.Size)
                                                        .Include(x => x.PreOrderProducts)
                                                           .ThenInclude(x => x.Product)
                                                        .Include(x => x.PreOrderProducts)
                                                           .ThenInclude(x => x.ColorPrimary)
                                                        .Include(x => x.PreOrderProducts)
                                                            .ThenInclude(x => x.ColorSecondary)
                                                        .Include(x => x.Client)
                                                        .Skip((@params.Page - 1) * @params.ItemsPerPage)
                                                        .Take(@params.ItemsPerPage).Where(data => !data.REMOVED).ToList();

                result.Data = _mapper.Map<List<PreOrderDtoGetMapped>>(preOrders);
                result.Header = header;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Erro al obtener: {ex.Message}";
            }

            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                bool isEditable = _preOrderRepository.PreOrderIsEditable(id);
                var preOrder = this._preOrderRepository.GetEntityToJoin(id)
                                        .Select(data => new
                                        {
                                            Id = data.ID,
                                            //Quantity = data.QUANTITY,
                                            Client = this._clientService.GetById(data.FK_CLIENT).Data,
                                            Items = this._preOrderProductService.GetByPreOrder(data.ID).Data,
                                            DateCreated = data.CREATED_AT,
                                            DateDelivery = data.DATE_DELIVERY,
                                            IsEditable = isEditable
                                        });
                result.Data = preOrder;
                result.Message = "Obtenido con exito";
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = $"Error al remover el pedido: {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetPreOrdersByRecentDate()
        {
            ServiceResult result = new();
            try
            {
                var report = _preOrderRepository.GetPreOrdersByRecentDate();
                result.Data = _mapper.Map<List<PreOrder>>(report);
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }
            return result;
        }

        public ServiceResult Remove(PreOrderDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                PreOrder preOrder = new PreOrder
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User
                };

                this._preOrderRepository.Remove(preOrder);
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = $"Error al remover el pedido: {ex.Message}";
            }
            return result;
        }

        public ServiceResult Update(PreOrderDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                PreOrder preOrder = new PreOrder
                {
                    ID = dtoUpdate.Id,
                    FK_CLIENT = dtoUpdate.FkClient,
                    USER_MOD = dtoUpdate.User,
                    COMPLETED = dtoUpdate.Completed
                };

                this._preOrderRepository.Update(preOrder);
                result.Message = "Actualizado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar el pedido: {ex.Message}";
            }
            return result;
        }
    }
}
