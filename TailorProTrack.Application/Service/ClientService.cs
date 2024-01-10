

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Client;
using TailorProTrack.Application.Extentions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class ClientService : IClientService
    {

        private readonly IClientRepository _repository;
        private readonly ILogger logger;
        //servicio de Phone
        private readonly IPhoneService _phoneService;

        //repositorios
        public ClientService(IClientRepository repository,
                            ILogger<IClientRepository> logger,
                            IConfiguration configuration,
                            IPhoneService phoneService)
        {
            this._repository = repository;
            this.logger = logger;
            this._phoneService = phoneService;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        public ServiceResult Add(ClientDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {

                dtoAdd.IsValid(this.Configuration);
                Client clientToAdd = new Client
                {
                    FIRST_NAME = dtoAdd.F_name,
                    LAST_NAME = dtoAdd.L_name,
                    FIRST_SURNAME = dtoAdd.F_surname,
                    LAST_SURNAME = dtoAdd.L_surname,
                    DNI = dtoAdd.Dni,
                    RNC = dtoAdd.Rnc
                };

                //agregando el cliente
                int id = this._repository.Save(clientToAdd);
                //agregando los telefonos
                //agregar logica en caso de que falle 
                result.Data = this._phoneService.AddMany(dtoAdd.phonesClient, id);
                //result.Data = id;
                result.Message = "Cliente registrado correctamente.";
            }catch(Exception ex) 
            {
                result.Success = false;
                result.Message = $"Error al registrar el cliente: {ex.Message}";
            }


            return result;
        }

        public ServiceResult GetAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var clients = this._repository.GetEntities()
                                              .Where(data => !data.REMOVED)
                                              .Select(data => new ClientDtoGet
                                              {
                                                  Id = data.ID,
                                                  F_name = data.FIRST_NAME,
                                                  L_name = data.LAST_NAME,
                                                  F_surname = data.FIRST_SURNAME,
                                                  L_surname = data.LAST_SURNAME,
                                                  Dni = data.DNI,
                                                  RNC = data.RNC,
                                              }).ToList();
                result.Data = clients;
                result.Message = "Cliente obtenidos correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los clientes: {ex.Message}";
            }


            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var client = this._repository.GetEntities()
                                             .Where(data=> !data.REMOVED && data.ID == id)
                                             .Select(data => new ClientDtoGetDetails
                                             {
                                                 Id = data.ID,
                                                 F_name = data.FIRST_NAME,
                                                 L_name = data.LAST_NAME,
                                                 F_surname = data.FIRST_SURNAME,
                                                 L_surname = data.LAST_SURNAME,
                                                 RNC = data.RNC,
                                                 Dni = data.DNI,
                                                 phones = this._phoneService.GetPhonesByIdClient(data.ID).Data
                                             });
                result.Data = client;
                result.Message = "Cliente obtenido correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener el cliente: {ex.Message}";
            }


            return result;
        }

        public ServiceResult Remove(ClientDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Client clientToRemove = new Client
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User,
                    MODIFIED_AT = dtoRemove.Date
                };

                //
                this._repository.Remove(clientToRemove);

                result.Message = "Cliente removido correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al remover el cliente: {ex.Message}";
            }


            return result;
        }

        public ServiceResult Update(ClientDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                dtoUpdate.IsValid(this.Configuration);
                Client clientToAdd = new Client
                {
                    FIRST_NAME = dtoUpdate.F_name,
                    LAST_NAME = dtoUpdate.L_name,
                    FIRST_SURNAME = dtoUpdate.F_surname,
                    LAST_SURNAME = dtoUpdate.L_surname,
                    DNI = dtoUpdate.Dni,
                    RNC = dtoUpdate.Rnc
                };
                //agregando el cliente
                this._repository.Update(clientToAdd);
                result.Message = "Cliente actualizado correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar el cliente: {ex.Message}";
            }


            return result;
        }
    }
}
