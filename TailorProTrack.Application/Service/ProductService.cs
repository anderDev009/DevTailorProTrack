using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Product;
using TailorProTrack.Application.Extentions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ITypeProdRepository _repositoryType;
        private readonly ILogger logger;
        public ProductService(IProductRepository repository,IConfiguration configuration,
            ILogger<ProductRepository> logger, ITypeProdRepository typeRepository)
        {
            _repository = repository;
            this.logger = logger;
            this.Configuration = configuration;
            this._repositoryType = typeRepository;
        }

        public IConfiguration Configuration;

        public ServiceResult Add(ProductDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                result = dtoAdd.IsProductValid(this.Configuration, this._repositoryType);

                if (!result.Success)
                {
                    return result;
                }
                Product product = new Product()
                {
                    NAME_PRODUCT = dtoAdd.name_prod,
                    DESCRIPTION_PRODUCT = dtoAdd.description,
                    SALE_PRICE = dtoAdd.sale_price,
                    USER_CREATED = dtoAdd.User,
                    CREATED_AT = dtoAdd.Date,
                    FK_TYPE = dtoAdd.fk_type
                };
                this._repository.Save(product);
                result.Message = "Insertado con exito";
            }catch(Exception ex)
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
                int registerCount = this._repository.GetEntities().Where(data => !data.REMOVED).Count();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);

                var products = this._repository.GetEntitiesPaginated(@params.Page, @params.ItemsPerPage)
                                                .Join(this._repositoryType.GetEntities(),
                                                      product => product.FK_TYPE,
                                                      typeProd => typeProd.ID,
                                                      (product, typeProd) => new { product, typeProd })
                                                .Where(data => !data.product.REMOVED)
                                                .GroupBy(data => data.product)
                                                .Select(data => new ProductDtoGet
                                                {
                                                    Id = data.Key.ID,
                                                    name_prod = data.Key.NAME_PRODUCT,
                                                    description = data.Key.DESCRIPTION_PRODUCT,
                                                    type = data.Select(d => d.typeProd.TYPE_PROD).FirstOrDefault(),
                                                    sale_price = data.Key.SALE_PRICE
                                                })
                                                .ToList();
                                 //.Join(this._repositoryType.GetEntities(),
                                 //       product => product.ID,
                                 //       typeProd => typeProd.ID,
                                 //       (product,typeProd) => 
                                 //       new {Product = product, Type_prod = typeProd})
                                 //.Where(data  => !data.Product.REMOVED)
                                 //.Select(data => new ProductDtoGet()
                                 //{
                                 //    Id = data.Product.ID,
                                 //    name_prod = data.Product.NAME_PRODUCT,
                                 //    sale_price = data.Product.SALE_PRICE,
                                 //    description = data.Product.DESCRIPTION_PRODUCT,
                                 //    type = data.Type_prod.TYPE_PROD
                                 //});
                result.Message = "Obtenidos con exito";
                result.Header = header;
                result.Data = products;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var product = this._repository.GetEntities()
                                               .Join(this._repositoryType.GetEntities(),
                                                      product => product.ID,
                                                      typeProd => typeProd.ID,
                                                      (product, typeProd) => new { product, typeProd })
                                                .Where(data => !data.product.REMOVED && data.product.ID == id)
                                                .Select(data => new ProductDtoGet
                                                {
                                                    Id = data.product.ID,
                                                    name_prod = data.product.NAME_PRODUCT,
                                                    description = data.product.DESCRIPTION_PRODUCT,
                                                    type = data.typeProd.TYPE_PROD,
                                                    sale_price = data.product.SALE_PRICE
                                                }); ;
                result.Data = product;
                result.Message = "Obtenido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }

        public ServiceResult Remove(ProductDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Product product = new Product()
                {
                    REMOVED = dtoRemove.Removed,
                    USER_MOD = dtoRemove.User,
                    MODIFIED_AT = dtoRemove.Date
                };
                this._repository.Remove(product);
                result.Message = "Removido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }

        public ServiceResult Update(ProductDtoUpdate dtoUpdate) 
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Product product = new Product()
                {
                    ID = dtoUpdate.Id,
                    SALE_PRICE = dtoUpdate.sale_price,
                    NAME_PRODUCT = dtoUpdate.name_prod,
                    DESCRIPTION_PRODUCT = dtoUpdate.description,
                    FK_TYPE  = dtoUpdate.fk_type,
                    USER_CREATED = dtoUpdate.User,
                    CREATED_AT = dtoUpdate.Date
                };
                this._repository.Update(product);
                result.Message = "Actualizado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }

        public Decimal GetPrice(int id)
        {
            decimal price;
            try
            {
                 price = this._repository.GetEntities().Where(d => d.ID == id)
                                                              .Select(data => data.SALE_PRICE)
                                                             .Single();
            }catch (Exception ex) 
            {
                price = 0;
            }
            return price;
        }
    }
}
