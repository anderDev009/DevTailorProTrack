using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        List<Client> GetAll();
        List<Client> FilterByDni(string dni);
        List<Client> FilterByRnc(string rnc);
        List<Client> FilterByFullName(string fullName);
        
    }
}
