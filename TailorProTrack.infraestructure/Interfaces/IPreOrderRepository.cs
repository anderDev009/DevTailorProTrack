

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IPreOrderRepository : IBaseRepository<PreOrder>
    {
        bool PreOrderIsEditable(int id);
    }
}
