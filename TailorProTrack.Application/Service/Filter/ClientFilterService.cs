
using System.Net;
using TailorProTrack.Application.Contracts.Client;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Client;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service.Filter
{
    public class ClientFilterService : IClientFilterService
    {
        private readonly IClientRepository _clientRepository;

        public ClientFilterService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public ServiceResult FilterByDni(string Dni)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var clients = this._clientRepository.FilterByDni(Dni).Select(data => new ClientDtoGet
                {
                    Id = data.ID,
                    F_name = data.FIRST_NAME,
                    L_name = data.LAST_NAME,
                    F_surname = data.FIRST_SURNAME,
                    L_surname = data.LAST_SURNAME,
                    Dni = data.DNI,
                    RNC = data.RNC,
                }); ;

                result.Data = clients;
                result.Message= "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Obtenidos con exito";
            }
            return result;
        }

        public ServiceResult FilterByFullName(string fullName)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var clients = this._clientRepository.FilterByFullName(fullName).Select(data => new ClientDtoGet
                {
                    Id = data.ID,
                    F_name = data.FIRST_NAME,
                    L_name = data.LAST_NAME,
                    F_surname = data.FIRST_SURNAME,
                    L_surname = data.LAST_SURNAME,
                    Dni = data.DNI,
                    RNC = data.RNC,
                });

                result.Data = clients;
                result.Message = "Obtenidos con exito";

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtenerlo {ex.Message}";
            }
            return result;
        }

        public ServiceResult FilterByRnc(string rnc)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var clients = this._clientRepository.FilterByRnc(rnc).Select(data => new ClientDtoGet
                {
                    Id = data.ID,
                    F_name = data.FIRST_NAME,
                    L_name = data.LAST_NAME,
                    F_surname = data.FIRST_SURNAME,
                    L_surname = data.LAST_SURNAME,
                    Dni = data.DNI,
                    RNC = data.RNC,
                }); ;

                result.Data = clients;
                result.Message = "Obtenidos con exito";
                ;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtenerlo {ex.Message}";
            }
            return result;
        }
    }
}
