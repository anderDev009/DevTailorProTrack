
using Microsoft.Extensions.Logging;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Phone;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class PhoneService : IPhoneService
    {
        private readonly IPhoneRepository _repository;
        private readonly ILogger logger;

        public PhoneService(IPhoneRepository repository, 
                            ILogger<IPhoneRepository> logger)
        {
            _repository = repository;
            this.logger = logger;
        }

        public ServiceResult Add(PhoneDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Phone phoneToAdd = new Phone 
                { 
                    NUMBER = dtoAdd.number,
                    USER_CREATED = dtoAdd.User,
                    TYPE = dtoAdd.type,
                    FK_CLIENT = dtoAdd.fk_client
                };

                this._repository.Save(phoneToAdd);
                result.Message = "Telefono agregado correctamente.";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar registrar el telefono {ex.Message}";
            }
    
            return result;
        }

        public ServiceResult AddMany(List<PhoneDtoAdd> dtoAdds, int fkClient)
        {
            ServiceResult result = new ServiceResult();
            try
            {

                //codigo para agregar
                List<Phone> phonesToAdd = new List<Phone>();
                foreach(var item in dtoAdds)
                {
                    phonesToAdd.Add(
                    new Phone
                    {
                        FK_CLIENT = fkClient,
                        NUMBER = item.number,
                        TYPE = item.type,
                        USER_CREATED = item.User
                    });
                }
                this._repository.SaveMany(phonesToAdd);

                result.Message = "Guardados con exito";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar agregarlos todos: {ex.Message}";
            }

            return result;
        }

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {

                int registerCount = this._repository.GetEntities().Where(d => !d.REMOVED).Count();  
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);


                var phones = this._repository.GetEntities().Where(data => !data.REMOVED)
                    .OrderBy(data => data.ID)
                    .Skip((@params.Page - 1) * @params.ItemsPerPage)
                    .Take(@params.ItemsPerPage)
                    .ToList();

                result.Data = phones;
                result.Header = header;
                result.Message = "Obtenidos correctamente.";
            }catch(Exception ex) 
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener los telefonos: {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var phone = this._repository.GetEntity(id);

                result.Data = phone;
                result.Message = "Obtenido correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener el telefono: {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetPhonesByIdClient(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var phones = this._repository.GetEntities()
                                .Where(data => data.FK_CLIENT == id)
                                .Select(data => new PhoneDtoGetByClient
                                {
                                    Id = data.ID,
                                    type = data.TYPE,
                                    number = data.NUMBER
                                })
                                .ToList();
                result.Data = phones;
                result.Message = "Obtenidos con exito.";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtenerlos por el id del cliente: {ex.Message}";
            }

            return result;
        }

        public ServiceResult Remove(PhoneDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Phone phoneToRemove = new Phone
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User
                };

                this._repository.Remove(phoneToRemove);
                result.Message = "Removido con exito.";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar remover el telefono: {ex.Message}";
            }
            return result;
        }

        public ServiceResult Update(PhoneDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Phone phoneToUpdate = new Phone()
                {
                    ID = dtoUpdate.Id,
                    USER_MOD = dtoUpdate.User,
                    TYPE = dtoUpdate.type,
                    NUMBER = dtoUpdate.number,
                    FK_CLIENT = dtoUpdate.fk_client
                };

                this._repository.Update(phoneToUpdate);
                result.Message = "Actualizado correctamente";
            }catch(Exception ex)
            {   
                result.Success = false;
                result.Message = $"Error al intentar actualizar el telefono: {ex.Message}";
            }
            return result;
        }
    }
}
