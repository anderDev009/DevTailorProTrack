
using TailorProTrack.Application.Dtos.PreOrderProducts;

namespace TailorProTrack.Application.Dtos.PreOrder
{
    public class PreOrderDtoAdd : PreOrderDtoBase
    {
        public int User {  get; set; }
        public DateTime DateDelivery {  get; set; }
        public bool? Itbis { get; set; }
		public List<PreOrderProductsDtoAdd> productsDtoAdds { get; set; }
    }
}
