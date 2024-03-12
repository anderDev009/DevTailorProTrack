
using TailorProTrack.Application.Dtos.TypeProd;

namespace TailorProTrack.Application.Dtos.Product
{
    public class ProductDtoGetMapped
    {
        public int ID { get; set; }
        public string NAME_PRODUCT { get; set; }
        public string DESCRIPTION_PRODUCT { get; set; }
        public double SALE_PRICE { get; set; }
        public DateTime LAST_REPLENISHMENT { get; set; }    
        public TypeProdDtoGetMapped? Type {  get; set; }
    }
}
