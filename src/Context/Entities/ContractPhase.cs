using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ContractPhase : Entity<int>
    {
        [Required]
        public int ContractId { get; set; }

        [Required]
        public long SequenceNumber { get; set; }

        [Required]
        public long PhaseNumber { get; set; }
        [Required]
        public bool Completed { get; set; }

        [Required]
        public bool Contested { get; set; }

        public int TimeDelay { get; set; }

        public Contract Contract { get; set; }

        public ICollection<PreTransaction> Transactions { get; set; }
    }
}
