

using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
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
            throw new NotImplementedException();
        }

        public ServiceResult GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Remove(BuyInventoryDtoRemove dtoRemove)
        {
            ServiceResult result = new();
            try 
            {
                BuyInventory buyInventory = _mapper.Map<BuyInventory>(dtoRemove);
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
