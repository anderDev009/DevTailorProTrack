

namespace TailorProTrack.Application.Dtos.Product
{
    public class BaseProduct : BaseDto
    {
            public string name_prod { get; set; }
            public string description {  get; set; }
            public decimal sale_price {  get; set; }
    }
}
