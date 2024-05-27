

using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Bank;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepository;

        public BankService(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }
        public ServiceResult Add(BankDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Bank bank = new Bank
                {
                    NAME = dtoAdd.Name,
                    USER_CREATED = dtoAdd.User
                    
                };
                this._bankRepository.Save(bank);
                result.Message = "Agregado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al agregarlo: {ex.Message}";
            }

            return result;
        }

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {
                int registerCount = this._bankRepository.CountEntities();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);
                var banks = this._bankRepository.GetEntitiesPaginated(@params.Page, @params.ItemsPerPage)
                                                .Select(data => new
                                                {
                                                    Id = data.ID,
                                                    BankName = data.NAME,
                                                }).ToList();

                result.Data = banks;
                result.Header = header;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Error al intentar obtenerlos";
            }

            return result;
        }

        public ServiceResult GetById(int id)
        {

            ServiceResult result = new ServiceResult();
            try
            {
                var bank = this._bankRepository.GetEntity(id);
                result.Data = bank;
                result.Message = "Obtenido con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al agregarlo: {ex.Message}";
                throw;
            }

            return result;
        }

        public ServiceResult Remove(BankDtoRemove dtoRemove)
        {

            ServiceResult result = new ServiceResult();
            try
            {
                Bank bank = new Bank
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User
                };
                this._bankRepository.Remove(bank);
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

        public ServiceResult Update(BankDtoUpdate dtoUpdate)
        {

            ServiceResult result = new ServiceResult();
            try
            {
                Bank bank = new Bank
                {
                    ID = dtoUpdate.Id,
                    NAME = dtoUpdate.Name,
                    USER_MOD = dtoUpdate.User
                };
                this._bankRepository.Update(bank);
                result.Message = "Actualizado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar: {ex.Message}";
                throw;
            }

            return result;
        }
    }
}
