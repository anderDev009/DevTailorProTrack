using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Inventory;

namespace TailorProTrack.Application.Contracts
{
    public interface IInventoryService : IBaseService<InventoryDtoAdd, InventoryDtoRemove,InventoryDtoUpdate>
    {
    }
}
