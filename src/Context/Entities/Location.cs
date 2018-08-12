using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public bool Verified { get; set; }

        public virtual GeoLocation GeoLocation { get; set; }

        public virtual ServiceRequest ServiceRequestDestination { get; set; }

        public virtual ServiceRequest ServiceRequestLocation { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Region Region { get; set; }

    }
}
