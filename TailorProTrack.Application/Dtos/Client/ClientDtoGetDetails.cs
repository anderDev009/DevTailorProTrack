using TailorProTrack.Application.Dtos.Phone;

namespace TailorProTrack.Application.Dtos.Client
{
    public class ClientDtoGetDetails : ClientDtoGet
    {
        public List<PhoneDtoGetByClient> phones { get; set; }
    }
}
