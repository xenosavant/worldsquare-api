using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Contract : UniqueEntity
    {
        [Required]
        public string EscrowAccountId { get; set; }
         [Required]
        public string DestAccountId { get; set; }
        [Required]
        public string SourceAccountId { get; set; }

        [Required]
        public long CurrentSequenceNumber { get; set; }

        [Required]
        public int ContractStateId { get; set; }

        [Required]
        public int ContractTypeId { get; set; }

        public virtual ContractState State { get; set; }

        public virtual ContractType Type { get; set; }

        public virtual ICollection<ContractPhase> Phases { get; set; }

        public virtual ServiceRequestFulfillment Fulfillment { get; set; }

        public virtual ProductShipment ProductShipment { get; set; }

    }
}
