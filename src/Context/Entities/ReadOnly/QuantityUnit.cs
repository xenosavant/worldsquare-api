﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class QuantityUnit : LookupData
    {
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}