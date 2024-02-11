
using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Order;
using TailorProTrack.Application.Exceptions;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationOrderExtention
    {
        public static void IsValid(this OrderDtoBase dtoBase,
                                                             IConfiguration configuration,
                                                             IClientRepository clientRepository,
                                                             IUserRepository userRepository)
        {
            ServiceResult result = new ServiceResult();
            if (!clientRepository.Exists(cl => cl.ID == dtoBase.FkClient))
            {
                throw new OrderServiceException(configuration["validations:clientDoesntExist"]);
            }
            //if (!userRepository.Exists(cl => cl.ID == dtoBase.FkUser))
            //{
            //    throw new OrderServiceException(configuration["validations:userDoesntExist"]);
            //}
        }

        public static void IsValidToAdd(this OrderDtoAdd dtoAdd,
                                        IConfiguration configuration,
                                        IClientRepository clientRepository,
                                        IUserRepository userRepository,
                                        IPreOrderRepository preOrderRepository,
                                        IPreOrderProductsRepository preOrderProductsRepository,
                                        IInventoryRepository inventoryRepository,
                                        IInventoryColorRepository inventoryColorRepository)
        {
            IsValid(dtoAdd, configuration, clientRepository, userRepository);

            if (!preOrderRepository.Exists(pr => pr.ID == dtoAdd.FkPreOrder))
            {
                throw new OrderServiceException(configuration["validations:preOrderDoesntExist"]);
            }

            var preOrderProducts = preOrderProductsRepository.GetByPreOrderId(dtoAdd.FkPreOrder);
            foreach (var product in preOrderProducts)
            {
                var productInventory = inventoryRepository.SearchEntities()
                    .Where(data => data.FK_PRODUCT == product.FK_PRODUCT && data.FK_SIZE == product.FK_SIZE).ToList();
                
                if(productInventory.Count == 0)
                {
                    throw new OrderServiceException(configuration["validations:DoesntHaveInventoryToAddOrder"]);
                }
                var inventoryColorProducts = inventoryColorRepository.SearchEntities()
                    .Where(data => data.FK_INVENTORY == productInventory.First().ID && data.FK_COLOR_PRIMARY == product.COLOR_PRIMARY
                    && data.FK_COLOR_SECONDARY == data.FK_COLOR_SECONDARY).ToList();

                if(inventoryColorProducts.Count == 0)
                {
                    throw new OrderServiceException(configuration["validations:DoesntHaveInventoryColorToAddOrder"]);
                }

                if(inventoryColorProducts.First().QUANTITY < product.QUANTITY)
                {
                    throw new OrderServiceException(configuration["validations:InsuficientInventoryColorToAddOrder"]);
                }
            }
        }
    }
}
