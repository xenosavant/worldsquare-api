using Stellmart.Api.Context.Entities.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Stellmart.Api.Context.Entities
{
    public abstract class Signature : EntityMaximum
    {
        [Required]
        public int PreTransactionId { get; set; }

        [Required]
        public bool Signed { get; set;  }

        [Required]
        public bool PreSign { get; set; }
        [Required]
        public string PublicKey { get; set; }
        public string SignatureHash { get; set; }

        public string SecretKeyHash { get; set; }

        public DateTime SignedOn { get; set; }

        public virtual PreTransaction Transaction { get; set; }
    }
}
