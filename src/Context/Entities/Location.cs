using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Context.Entities.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stellmart.Api.Context
{
    public class Location : EntityMaximum
    {
        public string Address { get; set; }

        public int GeoLocationId { get; set; }

        public string LocationComponentsFromApp { get; set; }
        public string LocationComponentsFromGoogleApi { get; set; }

        public string PlaceId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Required]
        public bool Verified { get; set; }

        public virtual GeoLocation GeoLocation { get; set; }

        public virtual ServiceRequest ServiceRequestDestination { get; set; }

        public virtual ServiceRequest ServiceRequestLocation { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Region Region { get; set; }
    }
}
