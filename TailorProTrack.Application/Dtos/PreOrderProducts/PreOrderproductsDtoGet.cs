

namespace TailorProTrack.Application.Dtos.PreOrderProducts
{
    public class PreOrderproductsDtoGet
    {
        public int Id { get; set; }
        public string ProductName { get; set; } 
        public int Quantity { get; set; }
        public string ColorPrimaryName { get; set; }
        public string ColorSecondaryName { get; set; }
        public string Size {  get; set; }
       
    }
}
