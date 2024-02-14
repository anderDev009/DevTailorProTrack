using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Inventory;
using TailorProTrack.Application.Exceptions;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationInventoryExtention
    {
        public static ServiceResult IsValid(this InventoryBaseDto baseDto,
                                            IConfiguration configuration,
                                            IProductRepository productRepository,
                                            ISizeRepository sizeRepository)
        {
            ServiceResult result = new ServiceResult();

            //validando producto
            if(!productRepository.Exists(product => product.ID == baseDto.fk_product))
            {
                throw new InventoryServiceException(configuration["productDoesntExist"]);
            }

            //validando size
            if(!sizeRepository.Exists(size => size.ID == baseDto.fk_size)){
                throw new InventoryServiceException(configuration["sizeDoesntExist"]);
            }
            //validando cantidad
            return result;
        }

        public static void IsValidToAdd(this InventoryDtoAdd dtoAdd,
            IConfiguration configuration, IProductRepository productRepository,
            ISizeRepository sizeRepository, IInventoryRepository inventoryRepository)
        {
            IsValid(dtoAdd,configuration, productRepository, sizeRepository);
            if(inventoryRepository.Exists(inventory => inventory.FK_SIZE == dtoAdd.fk_size && inventory.FK_PRODUCT == dtoAdd.fk_product))
            {
                throw new InventoryServiceException("Este producto ya esta registrado con este size.");
            }
        }
    }
}
