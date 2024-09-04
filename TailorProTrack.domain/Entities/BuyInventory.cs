using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class BuyInventory : BaseEntity
    {
        public string COMPANY {  get; set; }
        public string RNC { get; set; }
        public string? NCF {  get; set; }
        public DateTime DATE_MADE {  get; set; }
        public decimal TOTAL_SALE { get; set; }

        public bool? USED { get; set; }


        public List<Expenses?> Expenses { get; set; }
        public List<BuyInventoryDetail>? Details { get; set; }
    }
}
