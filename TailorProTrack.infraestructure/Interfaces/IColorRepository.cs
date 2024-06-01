using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface IColorRepository : IBaseRepository<Color>
    {
        List<Color> FilterByName(string name);
        List<Color> FilterByColorCode(string color);
        List<Color> FilterByProductAsociated(int productId);    
    }
}
