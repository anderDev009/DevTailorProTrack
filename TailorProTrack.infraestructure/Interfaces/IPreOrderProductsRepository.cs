

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IPreOrderProductsRepository : IBaseRepository<PreOrderProducts>
    {
        void SaveMany(List<PreOrderProducts> preOrderProducts); 
        List<PreOrderProducts> GetByPreOrderId(int PreOrderId);

        decimal GetAmountByIdPreOrder(int preOrderId);
        bool IsPreOrderProductInOrder(int id);

		List<PreOrderProducts> GetMissingColorsByIdPreOrder(int IdPreOrder);
        List<PreOrderProducts> GetMissingProducts();
        List<PreOrderProducts> GetPreOrderWithOrders(int IdPreOrder);
	}
}
