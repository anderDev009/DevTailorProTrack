using TailorProTrack.domain.Core;
namespace TailorProTrack.domain.Entities
{
    public class CodesDgi : BaseEntity
    {
        public int INITIAL_NUMBER { get; set; }
        public int END_NUMBER {get; set;}
        public int CURRENT_NUMBER { get; set; }
    }
}
