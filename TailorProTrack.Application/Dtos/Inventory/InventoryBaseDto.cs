using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailorProTrack.Application.Dtos.Inventory
{
    public class InventoryBaseDto : BaseDto
    {
        public DateTime last_replenishment {  get; set; }
    }
}
