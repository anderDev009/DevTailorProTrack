

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        int UpdatePassword(User user);
        int UpdateUsername(User user);  
    }
}
