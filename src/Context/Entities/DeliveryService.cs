using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class DeliveryService : Service
    {
        public int ServiceAreaId { get; set; }

        public virtual Area ServiceArea { get; set; }
    }
}
