using Stellmart.Api.Context.Entities.Entity;
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
        public double MeterRadius { get; set; }

        [Required]
        public int GeoLocationId { get; set; }

        public virtual GeoLocation GeoLocation { get; set; }


    }
}
