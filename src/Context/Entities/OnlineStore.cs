﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Stellmart.Api.Context.Entities
{
    public class OnlineStore : Service
    {
        [Required]
        public bool Global { get; set; }

        public bool Internal { get; set; }

        public int? ServiceRegionId { get; set; }

        public virtual Region ServiceRegion { get; set; }

        [ForeignKey("ServiceId")]
        public virtual ICollection<Listing> Listings { get; set; }

    }
}
