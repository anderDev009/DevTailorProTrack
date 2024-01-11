using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TailorProTrack.Application.Contracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Size;
using TailorProTrack.Application.Extentions;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;
using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Application.Service
{

    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _repository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IInventoryColorRepository _inventorySizeRepository;
        private ILogger logger;
        public SizeService(ISizeRepository repository,
            IInventoryRepository inventoryRepository,
            IInventoryColorRepository inventorySizeRepository,
            ILogger<SizeRepository> logger, 
            IConfiguration configuration)
        {
            _repository = repository;
            this.logger = logger;
            this._inventoryRepository = inventoryRepository;
            this._inventorySizeRepository = inventorySizeRepository;
            this.configuration = configuration;
        }
        private  IConfiguration configuration { get; }



        public ServiceResult Add(SizeDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //validaciones
                dtoAdd.IsSizeValid(this.configuration);

                if (!result.Success)
                {
                    return result;
                }
                //codigo de registrar un size 
                Size size = new Size
                {
                    CREATED_AT = dtoAdd.Date,   
                    USER_CREATED = dtoAdd.User,
                    SIZE = dtoAdd.size
                };
                this._repository.Save(size);
                result.Message = "Size registrado con exito";
            }catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar registrar el size: {ex.Message}";
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

                var sizes = this._repository.GetEntities().Where(d => !d.REMOVED)
                                            .OrderBy(d=> d.ID)
                                            .Skip((@params.Page - 1) * @params.ItemsPerPage)
                                            .Take(@params.ItemsPerPage);
                result.Data = sizes;
                result.Header = header;
                result.Message = "Sizes obtenidos correctamente";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al intentar obtener los sizes {ex}";
            }
            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var size = this._repository.GetEntity(id);
                result.Data = size;
                result.Message = "Size obtenido correctamente";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener el size";
            }
            return result;
        }

        public ServiceResult GetSizesAvailablesProductById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var sizesAvailables = this._repository.GetEntities()
                                                      .Join
                                                      (
                                                        this._inventoryRepository.GetEntities(),
                                                        size => size.ID,
                                                        inventory => inventory.FK_SIZE,
                                                        (size,inventory) => new{ size, inventory }
                                                      ).Where(data=> data.inventory.FK_PRODUCT == id)
                                                      .GroupBy(data=> new {data.inventory.ID,data.size.SIZE, data.inventory.QUANTITY})
                                                      .Select(group => new
                                                      {
                                                          idInventory = group.Key.ID,
                                                          size = group.Key.SIZE,
                                                          quantity = group.Key.QUANTITY
                                                      });
                //                .Join
                //(
                //this._inventoryRepository.GetEntities().Where(data => data.ID == id),
                //combined => .FK_INVENTORY,
                //inventory => inventory.ID,
                //(combined, inventory) => new { combined.size, combined.inventorySize, inventory }
                //)
                //.Where(data => !data.size.REMOVED)
                //.GroupBy(data => new { data.size.ID, data.size.SIZE })
                //.Select(group => new
                //{
                //    id = group.Key.ID,
                //    size = group.Key.SIZE,
                //    quantity = group.Select(data => data.inventorySize.QUANTITY)
                //});
                result.Message = "Obtenido correctamente";
                result.Data = sizesAvailables;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener los sizes disponibles";
            }
           
            return result;
        }

        public ServiceResult Remove(SizeDtoRemove dtoRemove)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Size sizeToRemove = new Size
                {
                    MODIFIED_AT = dtoRemove.Date,
                    USER_MOD = dtoRemove.User,
                    REMOVED = true

                };
                this._repository.Remove(sizeToRemove); 
                result.Message = "Size removido correctamente";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al remover el size {ex}";
            }

            return result;
        }

        public ServiceResult Update(SizeDtoUpdate dtoUpdate)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Size sizeToUpdate = new Size
                {
                    ID = dtoUpdate.Id,
                    MODIFIED_AT = dtoUpdate.Date,
                    USER_MOD = dtoUpdate.User,
                    SIZE = dtoUpdate.size

                };
                this._repository.Update(sizeToUpdate);
                result.Message = "Size actualizado correctamente";
            }catch(Exception ex)
            {
                result.Message = "Error al actualizar el size";
                result.Success = false;
            }
            return result;
        }
    }
}
