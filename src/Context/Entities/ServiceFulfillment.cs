using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Services.Interfaces;

namespace Stellmart.Api.Context.Entities
{
    public class ServiceFulfillment : Interaction
    {
        public int ServiceRequestFulfillmentId { get; set; }

        public virtual ServiceRequestFulfillment ServiceRequestFulfillment { get; set; }

        public override void Update(IContractService contractService)
        {
            // Update contract
        }
    }
}
