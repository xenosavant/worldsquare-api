using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class PreTransaction : Entity<int>
    {
        [Required]
        public int PreTransactionTypeId { get; set; }

        [Required]
        public bool Preauthorized { get; set; }

        public string XdrString { get; set; }

        [Required]
        public bool Submitted { get; set; }

        [Required]
        public int ContractPhaseId { get; set;  }

        public DateTime MinimumTime { get; set; }

        public DateTime MaximumTime { get; set; }

        public ContractPhase Phase { get; set; }

        public virtual ICollection<Signature> Signatures { get; set; }

    }
}
