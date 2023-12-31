using System.Collections.Specialized;

namespace TailorProTrack.Application.Dtos.Client
{
    public class ClientDtoGet
    {
        public int Id { get; set; }
        public string F_name { get; set; }
        public string L_name { get; set; }
        public string F_surname { get; set; }
        public string L_surname { get; set; }
        public string RNC { get; set; }
        public string Dni { get; set; }
    }
}
