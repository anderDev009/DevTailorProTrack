﻿
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
            //if (!preOrderRepository.Exists(pr => pr.ID == dtoAdd.FkPreOrder))
            //{
            //    throw new OrderServiceException(configuration["validations:preOrderDoesntExist"]);
            //}

			//en caso de que intente enviar mas de la cantidad solicitada
			//var preOrderProducts = preOrderProductsRepository.GetPreOrderWithOrders(dtoAdd.FkPreOrder);
			//foreach (var dto in dtoAdd.products)
			//{
   //             var invColor = inventoryColorRepository.GetEntity(dto.FkInventoryColor);
   //             var inventory = inventoryRepository.GetEntity(invColor.FK_INVENTORY);
			//	var preOrderProduct = preOrderProducts.Find(pr => pr.FK_PRODUCT == inventory.FK_PRODUCT && pr.FK_SIZE == inventory.FK_SIZE && pr.COLOR_PRIMARY == invColor.FK_COLOR_PRIMARY);
				
			//	if (preOrderProduct != null)
			//	{
			//		//var quantity = preOrderProduct.QUANTITY - preOrderProduct.QUANTITY;
			//		if (dto.Quantity > preOrderProduct.QUANTITY)
			//		{
			//			throw new OrderProductServiceException("No puedes poner mas de lo solicitado en el pedido.");
			//		}
			//	}
			//}
			//mensaje para enviar en caso de que se hayan insertado varios
			string message = "";
            //----
            //dtoAdd.products = new List<OrderProductDtoAdd>();
            //var preOrderProducts = preOrderProductsRepository.GetByPreOrderId(dtoAdd.FkPreOrder);
            bool addedOne = false;
            foreach(var product in dtoAdd.products)
            {
                var inventoryColorProducts = inventoryColorRepository.GetEntity(product.FkInventoryColor);
                if(inventoryColorProducts == null)
                {
                    throw new OrderProductServiceException("Inventario inexistente, favor revisar las referencias.");
                }
               
                if (inventoryColorProducts.QUANTITY < product.Quantity)
                {
                    throw new OrderProductServiceException("Uno o mas elementos no cumplen con la condicion para ser agregados, por favor revise las cantidades enviadas.");

                }

            }
            foreach (var product in dtoAdd.products)
            {
      

                
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

