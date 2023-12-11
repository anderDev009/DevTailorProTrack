using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Inventory : BaseEntity
    {
        public int FK_SIZE { get; set; }
        public int QUANTITY_AVAILABLE { get; set; }
        public DateTime LAST_REPLENISHMENT {  get; set; } 
    }
}
