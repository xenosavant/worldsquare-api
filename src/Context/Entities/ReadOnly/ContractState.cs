﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class ContractState : LookupData
    {
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}