

using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class PreOrder : BaseEntity
    {
        public int FK_CLIENT {  get; set; }
        public List<PreOrderProducts>? PreOrderProducts { get; set; }  
        public List<Order>? Order { get; set; }
        public Client? Client { get; set; }
    }
}
 