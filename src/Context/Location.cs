using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context
{
    public class Location : Entity<int>
    {
        public string Address { get; set; }

        public int GeoLocationId { get; set; }

        public string LocationComponents { get; set; }

        public string PlaceId { get; set; }

        public virtual GeoLocation GeoLocation { get; set; }

    }
}
