﻿using Microsoft.Extensions.Configuration;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos;
using TailorProTrack.Application.Dtos.InventoryColor;
using TailorProTrack.Application.Exceptions;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.Application.Extentions
{
    public static class ValidationInventoryColorExtention
    {

        public static void IsValidToRemove(this BaseDto dtoBase, IConfiguration configuration,IInventoryColorRepository rep)
        {
            if(!rep.Exists(data => data.ID == dtoBase.Id))
            {
                throw new InventoryColorServiceException(configuration["validations:doesntExist"]);
            }
        }
        public static ServiceResult IsValid(this InventoryColorDtoBase dtoBase, 
                                            IConfiguration configuration,
                                            IInventoryRepository inventoryRepository,
                                            IColorRepository colorRepository)
        {
            ServiceResult result = new ServiceResult();

            //comprobando la cantidad
            if(dtoBase.quantity <= 0)
            {
                throw new InventoryColorServiceException(configuration["validations:quantityLessZero"]);
            }

            //comprobando que la fk si exista
            if(!inventoryRepository.Exists(inventory => inventory.ID == dtoBase.fk_inventory ))
            {
                throw new InventoryColorServiceException(configuration["validations:inventoryDoesntExist"]);
            }

            //comprobando la validez del color primario
            if(!colorRepository.Exists(color => color.ID == dtoBase.fk_color_primary))
            {
                throw new InventoryColorServiceException(configuration["validations:colorDoesntExist"]);
            }

            //validez color secundario
            if(dtoBase.fk_color_secondary != 0)
            {
                if(!colorRepository.Exists(color => color.ID == dtoBase.fk_color_secondary))
                {
                    throw new InventoryColorServiceException(configuration["validations:colorDoesntExist"]);
                }
            }
            return result;
        }
    }
}
