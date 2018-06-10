using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class FulfillmentState : LookupData
    {
        public virtual ICollection<ServiceRequestFulfillment> Fulfillments { get; set; }
    }
}
