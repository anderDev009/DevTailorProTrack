
namespace TailorProTrack.Application.Dtos.PreOrder
{
    public class PreOrderDtoFkSizeFkProduct
    {
        public int FkSize { get; set; }
        public int FkProduct {  get; set; }

        public int FkColorPrimary { get; set; }
        public int? FkColorSecondary {  get; set; } 
    }
}
