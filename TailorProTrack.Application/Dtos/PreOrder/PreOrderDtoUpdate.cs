

namespace TailorProTrack.Application.Dtos.PreOrder
{
    public class PreOrderDtoUpdate : PreOrderDtoBase
    {
        public int Id { get; set; } 
        public int User {  get; set; }
        public bool Completed { get; set; }
}
    }
