
namespace TailorProTrack.domain.Core
{
    public abstract class BaseEntity
    {
        public int ID {  get; set; }
        public DateTime CREATED_AT {  get; set; }
        public DateTime MODIFIED_AT {  get; set; }
        public int USER_MOD { get; set; }
        public int USER_CREATED { get; set; }
        public bool REMOVED { get; set; }
    }
}
