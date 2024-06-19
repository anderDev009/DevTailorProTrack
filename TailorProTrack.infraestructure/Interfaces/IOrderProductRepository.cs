

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IOrderProductRepository : IBaseRepository<OrderProduct>
    {
        OrderProduct GetByForeignKeys(int idOrder, int idProduct);

        void SaveMany(List<OrderProduct> products);

        void ValidateMany(List<OrderProduct> products);

    
    }
}
