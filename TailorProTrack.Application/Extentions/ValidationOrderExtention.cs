
using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Order;
using TailorProTrack.Application.Dtos.OrderProduct;
using TailorProTrack.Application.Exceptions;
using TailorProTrack.domain.Entities;
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

        public static string IsValidToAdd(this OrderDtoAdd dtoAdd,
                                        IConfiguration configuration,
                                        IClientRepository clientRepository,
                                        IUserRepository userRepository,
                                        IPreOrderRepository preOrderRepository,
                                        IPreOrderProductsRepository preOrderProductsRepository,
                                        IInventoryRepository inventoryRepository,
                                        IInventoryColorRepository inventoryColorRepository,
                                        IOrderProductRepository orderProductRepository)
        {
            IsValid(dtoAdd, configuration, clientRepository, userRepository);
            if (!preOrderRepository.Exists(pr => pr.ID == dtoAdd.FkPreOrder))
            {
                throw new OrderServiceException(configuration["validations:preOrderDoesntExist"]);
            }
            //mensaje para enviar en caso de que se hayan insertado varios
            string message = "";
            //----
            //dtoAdd.products = new List<OrderProductDtoAdd>();
            //var preOrderProducts = preOrderProductsRepository.GetByPreOrderId(dtoAdd.FkPreOrder);
            bool addedOne = false;
            foreach (var product in dtoAdd.products)
            {
                //var productInventory = inventoryRepository.SearchEntities()
                //    .Where(data => data.FK_PRODUCT == product.FK_PRODUCT && data.FK_SIZE == product.FK_SIZE).ToList();

                //if (productInventory.Count != 0)
                //{

                //    var inventoryColorProducts = inventoryColorRepository.SearchEntities()
                //  .Where(data => data.FK_INVENTORY == productInventory.First().ID && data.FK_COLOR_PRIMARY == product.COLOR_PRIMARY
                //  && data.FK_COLOR_SECONDARY == data.FK_COLOR_SECONDARY).ToList();

                //    if (inventoryColorProducts.Count != 0 && !orderProductRepository.Exists(ordr => ordr.FK_INVENTORYCOLOR == inventoryColorProducts.First().ID))
                //    {
                var inventoryColorProducts = inventoryColorRepository.GetEntity(product.FkInventoryColor);
               
                if (inventoryColorProducts.QUANTITY >= product.Quantity)
                {
                    InventoryColor invColor = inventoryColorProducts;
                    invColor.QUANTITY -= product.Quantity;
                    inventoryColorRepository.Update(invColor);
                    inventoryRepository.UpdateQuantityInventory(invColor.FK_INVENTORY);
                    addedOne = true;
                }
                else
                {
                    message += $" {product.FkInventoryColor
                        },";
                }

            }
            if (!addedOne)
            {
                throw new OrderProductServiceException(configuration["validations:DoesntHaveInventoryToAddOrder"]);
            }
            return message;
        }



    }
}

