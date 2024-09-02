
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Suppliers;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Contracts
{
    public interface ISupplierService : IBaseServiceGeneric<SuppliersDtoAdd,SuppliersDtoUpdate,SuppliersDtoGet,Supplier>
    {
    }
}
