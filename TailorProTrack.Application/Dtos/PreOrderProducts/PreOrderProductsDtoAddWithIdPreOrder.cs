using TailorProTrack.infraestructure.Repositories;

namespace TailorProTrack.Application.Dtos.PreOrderProducts
{
    public class PreOrderProductsDtoAddWithIdPreOrder
    {
        public int FkPreOrder { get; set; }
		public int FkProduct { get; set; }
        public int FkSize { get; set; }
        public int Quantity { get; set; }
        public int FkColorPrimary { get; set; }
        public int FkColorSecondary { get; set; }
        public decimal customPrice { get; set; }

        public int User {  get; set; }

        public static bool IsValid(PreOrderProductsDtoAddWithIdPreOrder dto, PreOrderRepository preOrderRepository)
        {
	        if (!preOrderRepository.PreOrderIsEditable(dto.FkPreOrder))
	        {
		        return false;
	        }

	        return true;
        }
	}
}
