using TailorProTrack.Application.Contracts;
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
        private readonly ISizeRepository _sizeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IPreOrderProductsRepository _preOrderProductsRepository;


        //servicios 
        private readonly IPreOrderProductService _preOrderProductService;
        public PreOrderService(IPreOrderRepository preOrderRepository, ISizeRepository sizeRepository,
                        IProductRepository productRepository, IClientRepository clientRepository,
                        IPreOrderProductService preOrderProductService, IPreOrderProductsRepository preOrderProductsRepository)
        {
            _preOrderRepository = preOrderRepository;
            _sizeRepository = sizeRepository;
            _productRepository = productRepository;
            _clientRepository = clientRepository;
            _preOrderProductService = preOrderProductService;
            _preOrderProductsRepository = preOrderProductsRepository;
        }

        public ServiceResult Add(PreOrderDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                PreOrder preOrder = new PreOrder
                {
                    FK_CLIENT = dtoAdd.FkClient,

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

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {
                var countRegister = this._preOrderRepository.CountEntities();
                PaginationMetaData header = new PaginationMetaData(countRegister, @params.Page, @params.ItemsPerPage);

                var preOrders = this._preOrderRepository.GetEntities()
                                                        .Join
                                                        (
                                                        this._preOrderProductsRepository.GetEntities(),
                                                        preOrder => preOrder.ID,
                                                        preOrderProducts => preOrderProducts.FK_PREORDER,
                                                        (preOrder, products) => new { preOrder, products }
                                                        )
                                                        .GroupBy(data => data.preOrder.ID)
                                                        .Select
                                                        (group => new
                                                        {
                                                            Id = group.Key,
                                                            Client = this._clientRepository.GetEntity(group.Select(d => d.preOrder.FK_CLIENT)
                                                                                           .First()),
                                                            Items = group.Select(data => data.products).First()//el first veo si lo cambio
                                                        }
                                                        ).ToList();

                result.Data = preOrders;
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
                var preOrder = this._preOrderRepository.GetEntityToJoin(id)
                                        .Select(data => new
                                        {
                                            Id = data.ID,
                                            //Quantity = data.QUANTITY,
                                            Client = this._clientRepository.GetEntity(data.FK_CLIENT),
                                            Items = this._preOrderProductService.GetByPreOrder(data.ID)
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
