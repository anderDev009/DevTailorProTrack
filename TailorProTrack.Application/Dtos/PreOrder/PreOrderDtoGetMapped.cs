
using TailorProTrack.Application.Dtos.PreOrderProducts;

namespace TailorProTrack.Application.Dtos.PreOrder
{
    public class PreOrderDtoGetMapped
    {
        public int ID {  get; set; }
        public int FK_CLIENT { get; set; }
        public List<PreOrderProductDtoGetMapped>? PreOrderProducts { get; set; }
    }
}
