

namespace TailorProTrack.Application.Dtos.BuyInventoryDtos

{
    public class BuyInventoryDtoUpdate
    {
        public int ID { get; set; }
        public string COMPANY { get; set; }
        public string RNC { get; set; }
        public string? NCF { get; set; }
        public DateOnly DATE_MADE { get; set; }
    }
}
