
using TailorProTrack.Application.Dtos.Phone;

namespace TailorProTrack.Application.Dtos.Client
{
    public class ClientDtoAdd : ClientDtoBase
    {
        public List<PhoneDtoAdd> phonesClient { get; set; }
    }
}
