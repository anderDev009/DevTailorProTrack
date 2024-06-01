
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class BuyInventoryDetail : BaseEntity
    {

        public int FK_BUY_INVENTORY { get; set; }
        public int FK_PRODUCT { get; set; }
        public int QUANTITY { get; set; }
        public decimal PRICE { get; set; }
        public int FK_SIZE { get; set; }
        public int COLOR_PRIMARY { get; set; }
        public int COLOR_SECONDARY { get; set; }

        public BuyInventory? BuyInventory { get; set; }

        public Product? Product { get; set; }
        public Size? Size { get; set; }
        public Color? ColorPrimary { get; set; }
        public Color? ColorSecondary { get; set; }
    }
}
