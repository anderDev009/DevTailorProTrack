
using Microsoft.EntityFrameworkCore;

namespace TailorProTrack.domain.Entities
{
    public class OrderProduct
    {
        public int ID {  get; set; }
        public int FK_ORDER {  get; set; }
        public int FK_PRODUCT { get; set; }
        public int QUANTITY {  get; set; }

    }
}
