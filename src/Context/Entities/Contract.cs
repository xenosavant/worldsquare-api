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
        public long BaseSequenceNumber { get; set; }

        [Required]
        public long CurrentSequenceNumber { get; set; }

        [Required]
        public int ContractStateId { get; set; }

        [Required]
        public int ObligationId { get; set; }

        [Required]
        public int CurrentPhaseNumber { get; set; }

        // the amount of funding of the asset in stroops
        public int FundingAmount { get; set; }

        // the id of the asset for the funding
        public int FundingAssetId { get; set; }

        public virtual Asset FundingAsset { get; set; }

        public virtual ICollection<ContractPhase> Phases { get; set; }

        public virtual ServiceRequestFulfillment Fulfillment { get; set; }

        public virtual OrderItem OrderItem { get; set; }

        public virtual Obligation Obligation { get; set; }

    }
}
