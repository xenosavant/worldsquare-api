using Stellmart.Api.Context.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public abstract class PreTransaction : Entity<int>
    {
        public string XdrString { get; set; }

        public bool Submitted { get; set; }

        public int ContractPhaseId { get; set;  }
    }
}
