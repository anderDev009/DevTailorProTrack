

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.PreOrderProducts;

namespace TailorProTrack.Application.Contracts
{
    public interface IPreOrderProductService : IBaseService<PreOrderProductsDtoAdd, PreOrderProductsDtoRemove,
                                                            PreOrderProductsDtoUpdate>
    {
        ServiceResult SaveMany(List<PreOrderProductsDtoAdd> preOrderProducts, int FkPreOrder);
        ServiceResult GetByPreOrder(int orderId);
        ServiceResult GetDiffItems();
        ServiceResult AddWithFkPreOrder(PreOrderProductsDtoAdd dtoAdd,int id);
        decimal GetAmountByIdPreOrder(int IdPreOrder); // Get the total amount of a pre-order (sum of all products in the pre-order

		bool IsComplete(int IdPreOrder); 
    }
}
