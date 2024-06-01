

namespace TailorProTrack.Application.Dtos.PreOrderProducts
{
    public class PreOrderProductsDtoBase : BaseDto
    {
        public int FkProduct {  get; set; }
        public int FkSize { get; set; }
        public int Quantity { get; set; }
        public int FkColorPrimary { get; set; }
        public int FkColorSecondary { get; set; }
        public int FkPreOrder { get; set; } 
    }
}
