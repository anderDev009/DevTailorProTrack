﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailorProTrack.Application.Dtos.Inventory
{
    public class InventoryDtoUpdate : InventoryBaseDto
    {
        public int quantity { get; set; }

    }
}