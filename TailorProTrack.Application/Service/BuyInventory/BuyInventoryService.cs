

using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using TailorProTrack.Application.Contracts.BuyInventoryContracts;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.BuyInventoryDtos;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Service.BuyInventoryService
{
    public class BuyInventoryService : IBuyInventoryService
    {
        private readonly IBuyInventoryRepository _buyInventoryRepository;
        private readonly IBuyInventoryDetailRepository _buyInventoryDetailRepository;
        private readonly IMapper _mapper;
        public IConfiguration Configuration { get; }
        public BuyInventoryService(IBuyInventoryRepository buyInventoryRepository
            , IBuyInventoryDetailRepository buyInventoryDetailRepository
            , IMapper mapper,
            IConfiguration configuration)
        {
            _buyInventoryRepository = buyInventoryRepository;
            _buyInventoryDetailRepository = buyInventoryDetailRepository;
            _mapper = mapper;
            Configuration = configuration;
        }
        public ServiceResult Add(BuyInventoryDtoAdd dtoAdd)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                BuyInventory buyInventory = _mapper.Map<BuyInventory>(dtoAdd);
                List<BuyInventoryDetail> detailBuy = _mapper.Map<List<BuyInventoryDetail>>(dtoAdd.InventoryDetailDtoAdd);
                _buyInventoryRepository.AddBuyInventory(buyInventory, detailBuy);

            }catch (Exception ex)
            {
                serviceResult.Success = false;
                serviceResult.Message = $"Error: {ex.Message}";
            }
            return serviceResult;
          
        }

        public ServiceResultWithHeader GetAll(PaginationParams @params)
        {
            ServiceResultWithHeader result = new();
            try
            {
                int registerCount = this._buyInventoryRepository.CountEntities();
                PaginationMetaData header = new PaginationMetaData(registerCount, @params.Page, @params.ItemsPerPage);
                var buyInventory = this._buyInventoryRepository.SearchEntities()
                    .Include(b => b.Details)
                    //.ThenInclude(b => new { b.Product, b.Size, b.ColorPrimary,b.ColorSecondary})
                    .Skip((@params.Page - 1) * @params.ItemsPerPage).Take(@params.ItemsPerPage)
                    .OrderBy(buy => buy.DATE_MADE)
                    .ToList();

                result.Data = _mapper.Map<List<BuyInventoryDtoGet>>(buyInventory);
                result.Header = header;
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener las compras: {ex.Message}";
            }
            return result;
        }

        public ServiceResult GetById(int id)
        {
            ServiceResult result = new();
            try
            {
                var buy = _buyInventoryRepository.GetEntity(id);

                result.Data = _mapper.Map<BuyInventoryDtoGet?>(buy);
                result.Message = "Obtenidos con exito";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al obtener la compra: {ex.Message}";
            }
            return result;
        }

        public ServiceResult Remove(BuyInventoryDtoRemove dtoRemove)
        {
            ServiceResult result = new();
            try 
            {
                BuyInventory buyInventory = _mapper.Map<BuyInventory>(dtoRemove);
                //comprobando de que no se haya usado
                var buyInventoryDb = _buyInventoryRepository.GetEntity(buyInventory.ID);
                if ((bool)buyInventoryDb.USED)
                {
                    result.Message = "No se puede eliminar una compra que ya ha sido usada";
                    result.Success = true;
                    return result;
                }

                _buyInventoryRepository.Remove(buyInventory);
                result.Message = "Completado con exito";
            }catch(Exception ex)
            {
                result.Message = $"Error {ex.Message}";
                result.Success = false;
            }
            return result;
        }

        public ServiceResult Update(BuyInventoryDtoUpdate dtoUpdate)
        {
            ServiceResult result = new();
            try
            {
                BuyInventory buyInventory = _mapper.Map<BuyInventory>(dtoUpdate);
                _buyInventoryRepository.Update(buyInventory);
                result.Message = "Completado con exito";
            }catch(Exception ex)
            {
                result.Message = $"Error{ex.Message}";
                result.Success = false;
            }
            return result;
        }
    }
}
