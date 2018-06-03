using Stellmart.Api.Context.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Area : Entity<int>
    {
        public double MeterRadius { get; set; }

        public int GeoLocationId { get; set; }

        public virtual GeoLocation GeoLocation { get; set; }


    }
}
