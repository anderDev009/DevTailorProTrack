using System.Collections.Specialized;
using TailorProTrack.Application.Dtos.NoteCredit;
using TailorProTrack.Application.Dtos.Order;

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

        public bool? HasNoteCredit { get; set; }
        public decimal? AmountNoteCredit { get; set; }
    }
}
