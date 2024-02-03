

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.PreOrderProducts;

namespace TailorProTrack.Application.Contracts
{
    public interface IPreOrderProductService : IBaseService<PreOrderProductsDtoAdd, PreOrderProductsDtoRemove,
                                                            PreOrderProductsDtoUpdate>
    {
        ServiceResult SaveMany(List<PreOrderProductsDtoAdd> preOrderProducts, int FkPreOrder);
        ServiceResult GetByPreOrder(int orderId);
    }
}
