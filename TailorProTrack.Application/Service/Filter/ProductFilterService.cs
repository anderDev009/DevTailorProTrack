
using System.Diagnostics;
using TailorProTrack.Application.Contracts.Client;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Product;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service.Filter
{
    public class ProductFilterService : IProductFilterService
    {
        private readonly IProductRepository _productRepository;
        private readonly ITypeProdRepository _typeProdRepository;
        public ProductFilterService(IProductRepository productRepository,ITypeProdRepository typeProdRepository)
        {
            _productRepository = productRepository;
            _typeProdRepository = typeProdRepository;
        }

        public ServiceResult GetByHigherPrice(decimal price)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var products = this._productRepository.GetByHigherPrice(price).Join
                                                                             (this._typeProdRepository.GetEntities(),
                                                                             product => product.FK_TYPE,
                                                                             type => type.ID,
                                                                             (product, type) => new ProductDtoGet
                                                                             {
                                                                                 Id = product.ID,
                                                                                 name_prod = product.NAME_PRODUCT,
                                                                                 description = product.DESCRIPTION_PRODUCT,
                                                                                 sale_price = product.SALE_PRICE,
                                                                                 type = type.TYPE_PROD
                                                                             });

                result.Data = products;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los productos: {ex.Message}.";
            }
            return result;
        }

        public ServiceResult GetByMinorPrice(decimal price)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var products = this._productRepository.GetByMinorPrice(price).Join
                                                                             (this._typeProdRepository.GetEntities(),
                                                                             product => product.FK_TYPE,
                                                                             type => type.ID,
                                                                             (product,type) => new ProductDtoGet
                                                                             {
                                                                                 Id = product.ID,
                                                                                 name_prod = product.NAME_PRODUCT,
                                                                                 description = product.DESCRIPTION_PRODUCT,
                                                                                 sale_price = product.SALE_PRICE,
                                                                                 type = type.TYPE_PROD
                                                                             });

                result.Data = products;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los productos: {ex.Message}.";
            }
            return result;

        }

     
        public ServiceResult SearchByProductName(string productName)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var products = this._productRepository.SearchByName(productName).Join
                                                                             (this._typeProdRepository.GetEntities(),
                                                                             product => product.FK_TYPE,
                                                                             type => type.ID,
                                                                             (product, type) => new ProductDtoGet
                                                                             {
                                                                                 Id = product.ID,
                                                                                 name_prod = product.NAME_PRODUCT,
                                                                                 description = product.DESCRIPTION_PRODUCT,
                                                                                 sale_price = product.SALE_PRICE,
                                                                                 type = type.TYPE_PROD
                                                                             });

                result.Data = products;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los productos: {ex.Message}.";
            }
            return result;
        }

        public ServiceResult SearchByType(int fkType)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                var products = this._productRepository.SearchByType(fkType).Join
                                                                             (this._typeProdRepository.GetEntities(),
                                                                             product => product.FK_TYPE,
                                                                             type => type.ID,
                                                                             (product, type) => new ProductDtoGet
                                                                             {
                                                                                 Id = product.ID,
                                                                                 name_prod = product.NAME_PRODUCT,
                                                                                 description = product.DESCRIPTION_PRODUCT,
                                                                                 sale_price = product.SALE_PRICE,
                                                                                 type = type.TYPE_PROD
                                                                             });

                result.Data = products;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los productos: {ex.Message}.";
            }
            return result;
        }
    }
}

