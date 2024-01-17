
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.CategoryProd;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class CategorySizeService : ICategorySizeService
    {
        public readonly ICategorySizeRepository _repository;

        public CategorySizeService(ICategorySizeRepository repository)
        {
            _repository = repository;
        }

        public ServiceResult Add(CategoryProdDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                CategorySize categoryProd = new CategorySize
                {
                    CATEGORY = dtoAdd.Category,
                    USER_CREATED = dtoAdd.Id
                };
                this._repository.Save(categoryProd);
                result.Message = "Agregado con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener las categorias: {ex.Message}. ";
            }

            return result;
        }

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {
                int registerCount = this._repository.CountEntities();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);


                var categories = this._repository.GetEntitiesPaginated(@params.Page, @params.ItemsPerPage).Where(d => !d.REMOVED)
                                                 .OrderBy(d => d.ID)
                                                 .Select(data => new CategoryProdDtoGet
                                                 {
                                                    Id = data.ID,
                                                    Category = data.CATEGORY
                                                 })
                                                 .ToList();

                result.Data = categories;
                result.Header = header;
                result.Message = "obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar la categoria: {ex.Message} ";
            }

            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var category = this._repository.GetEntities().Where(d => d.ID == id).Select(data => new CategoryProdDtoGet
                {
                    Id = data.ID,
                    Category = data.CATEGORY
                });

                result.Data = category;
                result.Message = "Agregado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregar la categoria: {ex.Message} ";
            }

            return result;
        }

        public ServiceResult Remove(CategoryProdDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                CategorySize categorySize = new CategorySize
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User
                };
                this._repository.Remove(categorySize);
                result.Message = "Removido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar remover la categoria: {ex.Message} ";
            }

            return result;
        }

        public ServiceResult Update(CategoryProdDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                CategorySize categorySize = new CategorySize
                {
                    ID = dtoUpdate.Id,
                    CATEGORY = dtoUpdate.Category,
                    USER_MOD = dtoUpdate.User
                };
                this._repository.Update(categorySize);
                result.Message = "Actualizado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar actualizar la categoria: {ex.Message} ";
            }

            return result;
        }
    }
}
