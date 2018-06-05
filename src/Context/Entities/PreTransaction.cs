using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public abstract class PreTransaction : AuditableEntity<int>
    {
        public string XdrString { get; set; }

        [Required]
        public bool Submitted { get; set; }

        [Required]
        public int ContractPhaseId { get; set;  }

        public ContractPhase Phase { get; set; }

        public virtual ICollection<Signature> Signatures { get; set; }

    }
}
