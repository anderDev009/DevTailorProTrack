
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.BankAccount;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class BankAccountService : IBankAccountService
    {
        public readonly IBankAccountRepository _bankAccountRepository;
        public readonly IBankRepository _bankRepository;
        public ServiceResult Add(BankAccountDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                BankAccount bankAcc = new BankAccount
                {
                    USER_CREATED = dtoAdd.User,
                    FK_BANK = dtoAdd.FkBank,
                    BANK_ACCOUNT = dtoAdd.BankAccount,
                };

                this._bankAccountRepository.Save(bankAcc);
                result.Message = "Agregado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al agregar {ex.Message}";
            }
            return result;
        }

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {
                int registerCount = this._bankAccountRepository.CountEntities();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);
                var banksAccount = this._bankAccountRepository.GetEntitiesPaginated(@params.Page,@params.ItemsPerPage)
                                                               .Join(
                                                                this._bankRepository.GetEntities(),
                                                                bankAcc => bankAcc.FK_BANK,
                                                                bank => bank.ID,
                                                                (bankAcc, bank) => new { bankAcc, bank }
                                                                )
                                                                .Select(data => new BankAccountDtoGet
                                                                {
                                                                    Id = data.bankAcc.ID,
                                                                    Account = data.bankAcc.BANK_ACCOUNT,
                                                                    BankType = data.bank.NAME,
                                                                    Balance = data.bankAcc.BALANCE,
                                                                }).ToList();

                result.Header = header;
                result.Data = banksAccount;
                result.Message = "Obtenidos correctamente.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener: {ex.Message}";
            }
            return result; 
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var bankAccount = this._bankAccountRepository.GetEntityToJoin(id)
                                                             .Join(
                                                                this._bankRepository.GetEntities(),
                                                                bankAcc => bankAcc.FK_BANK,
                                                                bank => bank.ID,
                                                                (bankAcc,bank) => new {bankAcc,bank}
                                                                )
                                                                .Select(data => new BankAccountDtoGet
                                                                {
                                                                    Id = data.bankAcc.ID,
                                                                    Account = data.bankAcc.BANK_ACCOUNT,
                                                                    BankType = data.bank.NAME, 
                                                                    Balance = data.bankAcc.BALANCE,
                                                                });

                result.Data = bankAccount;
                result.Message = "Agregado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al agregar {ex.Message}";
            }
            return result;
        }

        public ServiceResult Remove(BankAccountDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                BankAccount bankAcc = new BankAccount
                {
                    ID =  dtoRemove.Id,
                    USER_MOD = dtoRemove.User,
                };

                this._bankAccountRepository.Remove(bankAcc);
                result.Message = "Agregado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al agregar {ex.Message}";
            }
            return result;
        }

        public ServiceResult Update(BankAccountDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                BankAccount bankAcc = new BankAccount
                {
                    USER_MOD = dtoUpdate.User,
                    FK_BANK = dtoUpdate.FkBank,
                    BANK_ACCOUNT = dtoUpdate.BankAccount,
                    BALANCE = dtoUpdate.Balance
                };

                this._bankAccountRepository.Update(bankAcc);
                result.Message = "Agregado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al agregar {ex.Message}";
            }
            return result;
        }
    }
}
