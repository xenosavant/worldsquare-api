using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public abstract class Service : EntityMaximum
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TagLine { get; set; }

        public int UserId { get; set; }

        bool Flagged { get; set; }

        [Required]
        public bool Verified { get; set; }

        [Required]
        public int NativeCurrencyId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [ForeignKey("NativeCurrencyId")]
        public virtual Currency NativeCurrency { get; set; }

        public ICollection<ServiceRequestFulfillment> Fulfillments { get; set; }

        public ICollection<Review> Reviews { get; set; }

    }
}
