
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Expenses;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service
{
    public class ExpensesService : IExpensesService
    {
        private readonly IExpensesRepository _expensesRepository;

        public ExpensesService(IExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
        }

        public ServiceResult Add(ExpensesDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Expenses expense = new Expenses
                {
                    AMOUNT = dtoAdd.Amount,
                    DESCR  = dtoAdd.Description,
                    USER_CREATED = dtoAdd.User,
                    NAME = dtoAdd.Name,
                    VOUCHER = dtoAdd.Voucher,
                };
                this._expensesRepository.Save(expense);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al agregar: {ex.Message}";
            }
            return result;
        }

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new ServiceResultWithHeader();
            try
            {
                int registerCount = this._expensesRepository.CountEntities();
                PaginationMetaData metadata = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);
                var expenses = this._expensesRepository.GetEntitiesPaginated(@params.Page, @params.ItemsPerPage)
                                                       .Select(data => new ExpensesDtoGet
                                                       {
                                                           Id = data.ID,
                                                           Amount = data.AMOUNT,
                                                           Name = data.NAME,
                                                           Description = data.DESCR,
                                                           Voucher = data.VOUCHER
                                                       })
                                                        .ToList();

                result.Header = metadata;
                result.Data = expenses;
                result.Message = "Obtenidos con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al agregar: {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var expense = this._expensesRepository.GetEntityToJoin(id).Select(data => new ExpensesDtoGet
                {
                    Id = data.ID,
                    Amount = data.AMOUNT,
                    Name = data.NAME,
                    Description = data.DESCR,
                    Voucher = data.VOUCHER
                });

                result.Data = expense;
                result.Message = "Obtenid con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener: {ex.Message}";
            }
            return result;
        }

        public ServiceResult Remove(ExpensesDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Expenses expense = new Expenses
                {
                    ID = dtoRemove.Id,
                    USER_MOD = dtoRemove.User
                };
                this._expensesRepository.Remove(expense);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar  : {ex.Message}";
            }
            return result;
        }

        public ServiceResult Update(ExpensesDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Expenses expense = new Expenses
                {
                    ID = dtoUpdate.Id,  
                    AMOUNT = dtoUpdate.Amount,
                    DESCR = dtoUpdate.Description,
                    USER_MOD = dtoUpdate.User,
                    NAME = dtoUpdate.Name,
                    VOUCHER = dtoUpdate.Voucher,
                };
                this._expensesRepository.Update(expense);
                result.Message = "Actualizado con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al agregar: {ex.Message}";
            }
            return result;
        }
    }
}
