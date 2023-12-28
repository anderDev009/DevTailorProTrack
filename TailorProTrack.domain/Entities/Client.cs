using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Client : BaseEntity
    {
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set;}

        public string FIRST_SURNAME { get; set; }
        public string LAST_SURNAME { get; set; }
        public string DNI {  get; set; }
        public string RNC { get; set; }
    }
}
