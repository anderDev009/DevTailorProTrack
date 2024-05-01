

using AutoMapper;
using TailorProTrack.Application.Core;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.Application.Service.BaseServices
{
    public class GenericService<DtoAdd, DtoUpdate, DtoGet, T> : IBaseServiceGeneric<DtoAdd, DtoUpdate, DtoGet, T>
        where DtoAdd : class
        where DtoUpdate : class
        where DtoGet : class
        where T : class
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<T> _repository;
        public GenericService(IMapper mapper, IBaseRepository<T> repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public ServiceResult Add(DtoAdd dtoAdd)
        {
            ServiceResult result = new();
            try
            {
                T entity = _mapper.Map<T>(dtoAdd);
                int id =_repository.Save(entity);
                result.Message = "Guardado con exito.";
                result.Data = id;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al almacenar: {ex.Message}";
            }
            return result;
        }

        public virtual ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new(); ;
            try
            {
                int registerCount = _repository.CountEntities();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);
                var banks = this._repository.GetEntitiesPaginated(@params.Page, @params.ItemsPerPage);

                result.Data =  _mapper.Map<List<DtoGet>>(banks);
                result.Header = header;
                result.Message = "Data obtenida con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener la data : {ex.Message}";
            }
            return result;
        }

        public ServiceResultWithHeader GetAllWithInclude(PaginationParams @params, List<string> properties)
        {
            ServiceResultWithHeader result = new(); ;
            try
            {
                int registerCount = _repository.CountEntities();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);
                var banks = this._repository.GetAllWithInclude(@params.Page, @params.ItemsPerPage,properties);

                result.Data = _mapper.Map<List<DtoGet>>(banks);
                result.Header = header;
                result.Message = "Data obtenida con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener la data : {ex.Message}";
            }
            return result;
        }

        public virtual ServiceResult GetById(int id)
        {
            ServiceResult result = new();
            try
            {
                T entity = _repository.GetEntity(id);
                result.Data = _mapper.Map<DtoGet>(entity);
                result.Message = "Obtenido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener la data: {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetByIdWithInclude(int id, List<string> properties)
        {
            ServiceResult result = new();
            try
            {
                T entity = _repository.GetByIdWithInclude(id,properties);
                result.Data = _mapper.Map<DtoGet>(entity);
                result.Message = "Obtenido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener la data: {ex.Message}";
            }
            return result;
        }

        public ServiceResult Remove(int ID)
        {
            ServiceResult result = new();
            try
            {
                T entity = _repository.GetEntity(ID);
                _repository.Remove(entity);
                result.Message = "Eliminado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar remover: {ex.Message}";
            }
            return result;
        }

        public ServiceResult Update(DtoUpdate dtoUpdate, int id)
        {
            ServiceResult result = new();
            try
            {
                T entity = _repository.GetEntity(id);
                entity = _mapper.Map<T>(dtoUpdate);
                _repository.Update(entity);
                result.Message = "Actualizado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar Actualizar: {ex.Message}";
            }
            return result;
        }
    }
}
