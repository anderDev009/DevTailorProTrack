using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Application.Dtos.PreOrderProducts
{
    public class PreOrderProductsDtoUpdate : PreOrderProductsDtoBase
    {
	    public static bool IsValid(PreOrderProductsDtoUpdate dto, PreOrderRepository preOrderRepository)
	    {
		    if (!preOrderRepository.PreOrderIsEditable(dto.FkPreOrder))
		    {
			    return false;
		    }

		    return true;
	    }
	}
}
