using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        void UpdateAmount(Order order);

        void UpdateStatusOrder(Order order);

    }
}
