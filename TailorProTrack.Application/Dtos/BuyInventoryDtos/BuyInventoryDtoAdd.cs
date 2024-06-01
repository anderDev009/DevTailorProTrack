

namespace TailorProTrack.Application.Dtos.BuyInventoryDtos
{
    public class BuyInventoryDtoAdd
    {
        public string COMPANY { get; set; }
        public string RNC { get; set; }
        public string? NCF { get; set; }
       
        public decimal? TOTAL_SALE { get; set; }

        public List<InventoryDetailDtoAdd> InventoryDetailDtoAdd { get; set; }
    }
}
