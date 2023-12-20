using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailorProTrack.Application.Dtos.Inventory
{
    public class InventoryBaseDto : BaseDto
    {
        public int fk_product {  get; set; }
        public int fk_size { get; set; }
        public int quantity {  get; set; }
    }
}
