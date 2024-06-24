

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IPreOrderRepository : IBaseRepository<PreOrder>
    {
        bool PreOrderIsEditable(int id);
        List<PreOrder> GetAccountsReceivable();
        List<PreOrder> GetPreOrdersByRecentDate();
        void Complete(int id);
        bool ChangeStatusPreOrder(int idPreOrder, bool status);
    }
}
