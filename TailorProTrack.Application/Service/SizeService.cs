using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using TailorProTrack.Application.Contracts.Size;
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
        private readonly ICategorySizeRepository _cateogoryRepository;
        private readonly IMapper _mapper;
        private ILogger logger;
        public SizeService(ISizeRepository repository,
            IInventoryRepository inventoryRepository,
            IInventoryColorRepository inventorySizeRepository,
            ICategorySizeRepository categoryRepository,
            ILogger<SizeRepository> logger, 
            IConfiguration configuration,
            IMapper mapper)
        {
            _repository = repository;
            this.logger = logger;
            this._inventoryRepository = inventoryRepository;
            this._inventorySizeRepository = inventorySizeRepository;
            this.configuration = configuration;
            _cateogoryRepository = categoryRepository;
            _mapper = mapper;
        }
        private  IConfiguration configuration { get; }



        public ServiceResult Add(SizeDtoAdd dtoAdd)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //validaciones
                dtoAdd.IsSizeValid(this.configuration,this._cateogoryRepository);

                if (!result.Success)
                {
                    return result;
                }
                //codigo de registrar un size 
                Size size = new Size
                {
                    CREATED_AT = dtoAdd.Date,   
                    USER_CREATED = dtoAdd.User,
                    SIZE =  dtoAdd.size.ToLower(),
                    FKCATEGORYSIZE = dtoAdd.FkCategory
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

                var sizes = this._repository.GetEntitiesPaginated(@params.Page, @params.ItemsPerPage).Where(d => !d.REMOVED)
                                            .Join
                                            (
                                                this._cateogoryRepository.GetEntities(),
                                                size => size.FKCATEGORYSIZE,
                                                category => category.ID,
                                                (size, category) => new { size, category }
                                            )
                                            .OrderBy(d => d.size.ID)
                                            .Select(data => new SizeDtoGet
                                            {
                                                Id = data.size.ID,
                                                Size = data.size.SIZE,
                                                Category = data.category.CATEGORY
                                            }).ToList();
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
                var size = this._repository.SearchEntities().Where(data => data.ID == id)
                    .Join(_cateogoryRepository.SearchEntities(), size => size.FKCATEGORYSIZE, category => category.ID,
                    (size, category) => new { size, category })
                    .Select(data => new SizeDtoGet
                    {
                        Id = data.size.ID,
                        Size = data.size.SIZE,
                        Category = data.category.CATEGORY
                    }).SingleOrDefault();
                result.Data = size;
                result.Message = "Size obtenido correctamente";
            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener el size";
            }
            return result;
        }

        public ServiceResult GetSizesAsociatedByProdId(int prodId)
        {
            ServiceResult result = new();
            try
            {
                var sizes = _repository.SizeByAsociatedProductId(prodId);
                result.Data = _mapper.Map<List<SizeDtoGetMapped>>(sizes);
                result.Message = "Obtenidos con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener: {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetSizesAvailablesProductById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var sizesAvailables = this._repository.GetEntities()
                                                      .Where(d => !d.REMOVED)
                                                      .Join
                                                      (
                                                        this._inventoryRepository.GetEntities().Where(d=>!d.REMOVED),
                                                        size => size.ID,
                                                        inventory => inventory.FK_SIZE,
                                                        (size,inventory) => new{ size, inventory }
                                                      )
                                                      .Join
                                                      (
                                                        _cateogoryRepository.GetEntities().Where(d=>!d.REMOVED),
                                                        group => group.size.FKCATEGORYSIZE,
                                                        sizeCategory => sizeCategory.ID,
                                                        (group,sizeC) => new {group.size,group.inventory,sizeC}
                                                      )
                                                      .Where(data=> data.inventory.FK_PRODUCT == id)
                                                      .GroupBy(data=> new {data.inventory.ID,data.size.SIZE, data.inventory.QUANTITY})
                                                      .Select(group => new
                                                      {
                                                          idInventory = group.Key.ID,
                                                          size = group.Key.SIZE,
                                                          quantity = group.Key.QUANTITY,
                                                          idCategory = group.Select(d => d.size.FKCATEGORYSIZE).First(),
                                                          nameCategory = group.Select(d => d.sizeC.CATEGORY).First()
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

        public ServiceResult GetSizesByCategoryId(int categoryId)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                
                var sizes = this._repository.GetEntities()
                                            .Where(d => !d.REMOVED && d.FKCATEGORYSIZE == categoryId)
                                            .Join
                                            (
                                                this._cateogoryRepository.GetEntities().Select(d=> new { d.ID,d.CATEGORY}),
                                                size => size.FKCATEGORYSIZE,
                                                category => category.ID,
                                                (size, category) => new { size, category }
                                            )
                                            .OrderBy(data => data.size.ID)
                                            .Select(data => new SizeDtoGetByCategory
                                            {
                                                Id = data.size.ID,
                                                Size = data.size.SIZE,
                                                CategoryId = data.category.ID,
                                                Category = data.category.CATEGORY
                                            });

                if (!sizes.Any()) throw new Exception("No se encontraron registros.");
                result.Data = sizes;
                result.Message = "Obtenidos con exito.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener los sizes: {ex.Message}.";
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
                    ID = dtoRemove.Id,
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
                dtoUpdate.IsSizeValidToAdd(this.configuration, this._cateogoryRepository,_repository);

                Size sizeToUpdate = new Size
                {
                    ID = dtoUpdate.Id,
                    MODIFIED_AT = dtoUpdate.Date,
                    USER_MOD = dtoUpdate.User,
                    SIZE = dtoUpdate.size,
                    FKCATEGORYSIZE =  dtoUpdate.FkCategory
                };
                this._repository.Update(sizeToUpdate);
                result.Message = "Size actualizado correctamente";
            }catch(Exception ex)
            {
                result.Message = $"Error al actualizar el size: {ex.Message}";
                result.Success = false;
            }
            return result;
        }
    }
}
