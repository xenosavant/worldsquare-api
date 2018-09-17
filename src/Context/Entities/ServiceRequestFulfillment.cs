using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public abstract class ServiceRequestFulfillment : EntityMaximum
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int ServiceRequestId { get; set; }

        [Required]
        public int ContractId { get; set; }

        [Required]
        public int FulfillmentStateId { get; set; }

        public virtual Service Service { get; set; }

        public virtual ServiceRequest ServiceRequest { get; set; }

        public virtual ServiceFulfillment ServiceFulfillment { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual FulfillmentState FulfillmentState {get; set;}
    }
}
