using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Dtos.Inventory
{
    public class InventoryDtoGet
    {
        public int id {  get; set; }
        public string? product_name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; } 
        public string last_replenishment { get; set; }

        public object? availableSizes { get; set; }
    }
}
