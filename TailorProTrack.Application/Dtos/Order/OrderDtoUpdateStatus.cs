

namespace TailorProTrack.Application.Dtos.Order
{
    public class OrderDtoUpdateStatus : BaseDto
    {
        public bool Checked { get; set; }
        public string? StatusOrder {  get; set; }
    }
}
