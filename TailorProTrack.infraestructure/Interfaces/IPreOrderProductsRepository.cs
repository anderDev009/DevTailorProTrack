

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IPreOrderProductsRepository : IBaseRepository<PreOrderProducts>
    {
        void SaveMany(List<PreOrderProducts> preOrderProducts); 
        List<PreOrderProducts> GetByPreOrderId(int PreOrderId);

        decimal GetAmountByIdPreOrder(int preOrderId);
        List<PreOrderProducts> GetMissingColorsByIdPreOrder(int IdPreOrder);
    }
}
