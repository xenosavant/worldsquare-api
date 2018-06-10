using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public abstract class ServiceRequest : EntityMaximum
    {
        public int RequestorId { get; set; }

        public bool Fulfilled { get; set; }

        public int? DestinationId { get; set; }

        public int? LocationId { get; set; }

        public virtual ApplicationUser Requestor { get; set; }

        [ForeignKey("DestinationId")]
        public virtual Location Destination { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        public virtual ICollection<ServiceRequestFulfillment> Fulfillments { get; set; }
    }
}
