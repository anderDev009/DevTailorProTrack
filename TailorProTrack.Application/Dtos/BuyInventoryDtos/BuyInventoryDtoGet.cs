

using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Dtos.BuyInventoryDtos
{
    public class BuyInventoryDtoGet
    {
        public int ID { get; set; }
        public string COMPANY { get; set; }
        public string RNC { get; set; }
        public string? NCF { get; set; }
        public DateTime DATE_MADE { get; set; }
        public decimal TOTAL_SALE { get; set; }

        public bool USED { get; set; }

        public List<inventoryDetailDtoGet>? Details { get; set; }
    }
}
