using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ServiceRequest : EntityMaximum
    {
        public int RequestorId { get; set; }

        public bool Fulfilled { get; set; }

        public virtual ApplicationUser Requestor { get; set; }

    }
}
