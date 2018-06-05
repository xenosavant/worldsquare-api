using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class GeoLocation : Entity<int>
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
