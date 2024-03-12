

namespace TailorProTrack.Application.Dtos.BuyInventoryDtos

{
    public class InventoryDetailDtoAdd
    {
        public int? FK_BUY_INVENTORY { get; set; }
        public int FK_PRODUCT { get; set; }
        public int QUANTITY { get; set; }
        public decimal PRICE { get; set; }
        public int FK_SIZE { get; set; }
        public int COLOR_PRIMARY { get; set; }
        public int COLOR_SECONDARY { get; set; }
    }
}
