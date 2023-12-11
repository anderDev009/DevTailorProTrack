using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TailorProTrack.domain.Core
{
    public class BaseEntity
    {
        public int ID {  get; set; }
        public DateTime CREATED_AT {  get; set; }
        public DateTime MODIFIED_AT {  get; set; }
        public int USER_MOD { get; set; }
        public int USER_CREATED { get; set; }
        public bool REMOVED { get; set; }
    }
}
