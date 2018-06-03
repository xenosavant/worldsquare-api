using Stellmart.Api.Context.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Contract : Entity<int>
    {
        public string EscrowAccountId { get; set; }

        public int CurrentSequenceNumber { get; set; }

        public int ContractStateId
    }
}
