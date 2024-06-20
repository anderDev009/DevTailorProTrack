


namespace TailorProTrack.Application.Dtos.Order
{
    public class OrderDtoBase : BaseDto
    {
        public int FkClient {  get; set; }
        public int FkUser {  get; set; }
        public bool Checked { get; set; }
        public int FkPreOrder { get; set; }
        public string DescriptionJob { get; set; }
        public string? Observation { get; set; }
    }
}
