using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface ISizeRepository : IBaseRepository<Size>
    {
        List<Size> FilterByName(string name);
        List<Size> FilterByIdCategory(int categoryId);  
    }
}
