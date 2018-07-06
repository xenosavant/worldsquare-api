using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Area : AuditableEntity<int>
    {
        [Required]
        public double Radius { get; set; }

        [Required]
        public int GeoLocationId { get; set; }

        [Required]
        public int DistanceUnitTypeId { get; set; }

        public virtual GeoLocation GeoLocation { get; set; }

        public virtual DistanceUnit DistanceUnit { get; set; }

        public virtual ICollection<DeliveryService> DeliveryServices { get; set; }



    }
}
