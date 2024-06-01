
namespace TailorProTrack.Application.Dtos.Phone
{
    public class PhoneDtoBase : BaseDto
    {
        public string type { get; set; }
        public string number { get; set; }

        public int fk_client {  get; set; }
    }
}
