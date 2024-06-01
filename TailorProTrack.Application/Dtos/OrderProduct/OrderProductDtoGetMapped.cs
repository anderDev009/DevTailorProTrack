using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.Application.Dtos.InventoryColor;
using TailorProTrack.Application.Dtos.Order;

namespace TailorProTrack.Application.Dtos.OrderProduct
{
    public class OrderProductDtoGetMapped
    {
        public int FK_ORDER { get; set; }
        public int FK_INVENTORYCOLOR { get; set; }
        public int QUANTITY { get; set; }

        //public OrderDtoGetMapped? Order { get; set; }
        //public InventoryColorDtoGet? InventoryColor { get; set; }
    }
}
