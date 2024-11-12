

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface ISupplierRepository : IBaseRepository<Supplier>
    {
        public bool ExistSupplierByRnc(string rnc); 
    }
}
