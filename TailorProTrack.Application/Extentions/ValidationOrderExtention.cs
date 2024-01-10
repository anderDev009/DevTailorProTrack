
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
            if (!userRepository.Exists(cl => cl.ID == dtoBase.FkUser))
            {
                throw new OrderServiceException(configuration["validations:userDoesntExist"]);
            }
        }
    }
}
