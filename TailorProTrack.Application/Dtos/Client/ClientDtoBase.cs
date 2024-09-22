

namespace TailorProTrack.Application.Dtos.Client
{
    public class ClientDtoBase : BaseDto
    {
        public string F_name {  get; set; }
        public string? L_name { get; set; }
        public string F_surname { get; set; }
        public string? L_surname { get; set; }
        public string? Rnc { get; set; }
        public string? Dni {  get; set; }

    }
}
