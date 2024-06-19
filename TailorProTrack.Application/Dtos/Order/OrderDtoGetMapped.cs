using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.Application.Dtos.Client;
using TailorProTrack.Application.Dtos.OrderProduct;
using TailorProTrack.Application.Dtos.PreOrder;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Dtos.Order
{
    public class OrderDtoGetMapped
    {
        public int ID { get; set; }
        public int FK_CLIENT {  get; set; }
        public int FK_PREORDER { get; set; }
        public bool CHECKED {  get; set; }
        public decimal AMOUNT { get; set; }
        public string SEND_TO { get; set; }
        public string? OBSERVATION { get; set; }    
		public string DESCRIPTION_JOB { get; set; }
        public string? STATUS_ORDER { get; set; }

        public ClientDtoGet? Client { get; set; }
        public PreOrderDtoGetMapped? PreOrder { get; set; }
        public List<OrderProductDtoGetMapped>? OrderProducts { get; set; }

    }
}
