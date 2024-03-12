
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Product : BaseEntity
    {
        public string NAME_PRODUCT { get; set; }
        public string DESCRIPTION_PRODUCT {  get; set; }
        public decimal SALE_PRICE { get; set; }
        public int FK_TYPE { get; set; }
        
        public DateTime LAST_REPLENISHMENT {  get; set; }

        public Type_prod? Type { get; set; }
        public List<BuyInventoryDetail>? productsInBuys { get; set; }
    }
}
