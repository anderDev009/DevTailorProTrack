using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailorProTrack.domain.Entities
{
    public class InventorySize
    {
        public int FK_SIZE { get; set; }
        public int FK_INVENTORY { get; set; }

        public int QUANTITY { get; set; }
    }
}
