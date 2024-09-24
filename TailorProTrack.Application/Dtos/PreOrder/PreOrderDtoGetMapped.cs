
using TailorProTrack.Application.Dtos.Client;
using TailorProTrack.Application.Dtos.Order;
using TailorProTrack.Application.Dtos.PreOrderProducts;

namespace TailorProTrack.Application.Dtos.PreOrder
{
    public class PreOrderDtoGetMapped
    {
        public int ID {  get; set; }
        public int FK_CLIENT { get; set; }
        public DateTime DateDelivery {  get; set; }
        public DateTime DateCreated {  get; set; }
        //campo para saber is es editable el pedido
        public bool? IsEditable { get; set; }
        public decimal? Amount { get; set; }
        public bool? Itbis { get; set; }
        public decimal? AmountBase { get; set; }
        public bool? Finished { get; set; }
        public bool? IsCompleted { get; set; }
        //Client props
		public List<PreOrderProductDtoGetMapped>? PreOrderProducts { get; set; }
        public List<OrderDtoGetMapped>? Order { get; set; }
        public ClientDtoGet? Client { get; set; }
    }
}
