using TailorProTrack.Application.Dtos.Color;
using TailorProTrack.Application.Dtos.Product;
using TailorProTrack.Application.Dtos.Size;

namespace TailorProTrack.Application.Dtos.BuyInventoryDtos
{
    public class inventoryDetailDtoGet
    {
        public int ID { get; set; }
        public int FK_BUY_INVENTORY { get; set; }
        public int FK_PRODUCT {  get; set; }
        public double PRICE {  get; set; }
        public int QUANTITY { get; set; }

        public ProductDtoGetMapped? Product { get; set; }
        public SizeDtoGetMapped? Size { get; set; }
        public ColorDtoGetMapped? ColorPrimary { get; set; }
        public ColorDtoGetMapped? ColorSecondary { get; set;  }

    }
}
