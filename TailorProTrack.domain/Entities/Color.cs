using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Color : BaseEntity
    {
        public string COLORNAME { get; set; }
        public string CODE_COLOR {  get; set; }

        public List<BuyInventoryDetail>? ColorPrimaryInBuys { get; set; }
        public List<BuyInventoryDetail>? ColorSecondaryInBuys { get; set; }

        public List<PreOrderProducts>? ColorPrimaryInPreOrder {  get; set; }
        public List<PreOrderProducts>? ColorSecondaryInPreOrder { get; set; }
        public List<ProductColor>? ProductColor {  get; set; }
    }
}
