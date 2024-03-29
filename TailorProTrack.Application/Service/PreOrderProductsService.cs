
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.PreOrderProducts;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class PreOrderProductsService : IPreOrderProductService
    {
        private readonly IPreOrderProductsRepository _preOrderProductRepository;
        //repositorio de productos
        private readonly IProductRepository _productRepository;
        //repositorio size
        private readonly ISizeRepository _sizeRepository;
        //repositorio colores 
        private readonly IColorRepository _colorRepository;
        public PreOrderProductsService(IPreOrderProductsRepository preOrderProductsRepository,
                                       IProductRepository productRepository,
                                       ISizeRepository sizeRepository,
                                       IColorRepository colorRepository)
        {
            _preOrderProductRepository = preOrderProductsRepository;
            _productRepository = productRepository;
            _sizeRepository = sizeRepository;
            _colorRepository = colorRepository;

        }
        public ServiceResult Add(PreOrderProductsDtoAdd dtoAdd)
        {
            throw new NotImplementedException();
        }

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResult GetByPreOrder(int orderId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var colors = this._colorRepository.GetEntities();
                var preOrderProducts = this._preOrderProductRepository.GetByPreOrderId(orderId)
                                                                      .Join
                                                                      (
                                                                       _productRepository.GetEntities().Select(data => new { data.ID, data.SALE_PRICE, data.NAME_PRODUCT }),
                                                                       preOrderProducts => preOrderProducts.FK_PRODUCT,
                                                                       product => product.ID,
                                                                       (preOrderProducts, product) => new { preOrderProducts, product }
                                                                      )
                                                                      .Join
                                                                      (
                                                                      _sizeRepository.GetEntities(),
                                                                      group => group.preOrderProducts.FK_SIZE,
                                                                      size => size.ID,
                                                                      (group, size) => new { group.preOrderProducts, group.product, size }
                                                                      )
                                                                      //color primary;
                                                                      .Join
                                                                      (
                                                                        colors,
                                                                        group => group.preOrderProducts.COLOR_PRIMARY,
                                                                        colorPrimary => colorPrimary.ID,
                                                                        (group, colorPrimary) => new { group.preOrderProducts, group.product, group.size, colorPrimary }
                                                                      )
                                                                      .Join
                                                                      (
                                                                        colors,
                                                                        group => group.preOrderProducts.COLOR_SECONDARY,
                                                                        colorSecondary => colorSecondary.ID,
                                                                        (group, colorSecondary) => new { group.preOrderProducts, group.product, group.size, group.colorPrimary, colorSecondary }
                                                                      )
                                                                      .Select(group => new
                                                                      {
                                                                          Id = group.preOrderProducts.ID,
                                                                          ProductId = group.product.ID,
                                                                          ProductName = group.product.NAME_PRODUCT,
                                                                          Price = group.product.SALE_PRICE,
                                                                          SizeId = group.size.ID,
                                                                          Size = group.size.SIZE,
                                                                          Quantity = group.preOrderProducts.QUANTITY,
                                                                          ColorPrimaryId = group.colorPrimary.ID,
                                                                          ColorPrimary = group.colorPrimary.COLORNAME,
                                                                          ColorSecondaryId = group.colorSecondary.ID,
                                                                          ColorSecondary = group.colorSecondary.COLORNAME
                                                                      });
                result.Data = preOrderProducts;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Data = $"Error al obtener el pedido: {ex.Message}";
            }
            return result;
        }
        //metodo para saber si es posible realizarle una orden a un pedido o indicar que dicho pedido esta completo
        //modificar logica
        public bool IsComplete(int IdPreOrder)
        {
            try
            {
                var listProducts = _preOrderProductRepository.GetByPreOrderId(IdPreOrder);
                //comprobando que no contenga productos
                if(listProducts.Count > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ServiceResult Remove(PreOrderProductsDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                PreOrderProducts preOrderProduct = new PreOrderProducts
                {
                    USER_MOD = dtoRemove.User,
                    ID = dtoRemove.Id
                };
                this._preOrderProductRepository.Remove(preOrderProduct);
                result.Message = "Removido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al remover: {ex.Message}";
                throw;
            }

            return result;
        }

        public ServiceResult SaveMany(List<PreOrderProductsDtoAdd> preOrderProducts, int FkPreOrder)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                List<PreOrderProducts> preOrderP = new List<PreOrderProducts>();
                foreach (var product in preOrderProducts)
                {
                    preOrderP.Add(new PreOrderProducts
                    {
                        COLOR_PRIMARY = product.FkColorPrimary,
                        COLOR_SECONDARY = product.FkColorSecondary,
                        FK_PRODUCT = product.FkProduct,
                        FK_SIZE = product.FkSize,
                        QUANTITY = product.Quantity,
                        USER_CREATED = product.User,
                        CREATED_AT = DateTime.Now,
                        FK_PREORDER = FkPreOrder
                    });
                }

                this._preOrderProductRepository.SaveMany(preOrderP);
                result.Message = "Agregados con exito";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregarlos {ex.Message}";
            }

            return result;
        }

        public ServiceResult Update(PreOrderProductsDtoUpdate dtoUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
