
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Client;

namespace TailorProTrack.Application.Contracts
{
    public interface IClientService : IBaseService<ClientDtoAdd,ClientDtoRemove,ClientDtoUpdate>
    { 
    }
}
