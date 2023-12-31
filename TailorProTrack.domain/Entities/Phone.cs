
using System.Runtime.InteropServices.Marshalling;
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Phone : BaseEntity
    {
        public string TYPE { get; set; }
        public string NUMBER { get; set; }

        public int FK_CLIENT { get; set; }
    }
}
