using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Client;

namespace TailorProTrack.Application.Contracts.Client
{
    public interface IClientService : IBaseService<ClientDtoAdd, ClientDtoRemove, ClientDtoUpdate>
    {
        ServiceResult GetAll();
    }
}
