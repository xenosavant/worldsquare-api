﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class TradeInState : LookupData
    {
        public virtual ICollection<TradeItem> TradeItems { get; set; }
    }
}
