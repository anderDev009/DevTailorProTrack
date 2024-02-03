
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Brand;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public ServiceResult Add(BrandDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Brand brand = new Brand
                {
                    NAME = dtoAdd.Name,
                    USER_CREATED = dtoAdd.User
                };
                this._brandRepository.Save(brand);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al agregar una marca: {ex.Message}";
            }
            return result;
        }

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {
                int registerCount = this._brandRepository.CountEntities();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);

                var brands = this._brandRepository.GetEntitiesPaginated(@params.Page, @params.ItemsPerPage).Select(data => new
                {
                    Id = data.ID,
                    Brand = data.NAME
                }).ToList();

                result.Data = brands;
                result.Message = "Marcas obtenidas correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener las marcas: {ex.Message}";
            }

            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var brand = this._brandRepository.GetEntity(id);
                result.Data = brand;
                result.Message = "Obtenido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener la marca: {ex.Message}";
            }
            return result;

        }

        public ServiceResult Remove(BrandDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Brand brand = new Brand
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User
                };
                this._brandRepository.Remove(brand);
                result.Message = "Removido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener la marca: {ex.Message}";
            }
            return result;
        }

        public ServiceResult Update(BrandDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                Brand brand = new Brand
                {
                    ID = dtoUpdate.Id,
                    NAME = dtoUpdate.Name,
                    USER_MOD = dtoUpdate.User
                };
                this._brandRepository.Update(brand);
                result.Message = "Actualizado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar la marca: {ex.Message}";
            }
            return result;
        }
    }
}
