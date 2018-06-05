using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class OnlineStore : Service
    {
        [Required]
        public bool Global { get; set; }

        [Required]
        public int LocationId { get; set; }

        public int ServiceRegionId { get; set;  }

        public virtual Location Location { get; set; }

        public virtual Region ServiceRegion{ get; set; }



    }
}
