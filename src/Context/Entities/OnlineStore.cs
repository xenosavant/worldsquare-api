using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int ItemMetaDateId { get; set; }

        [NotMapped]
        public IEnumerable<Review> Reviews => OnlineStoreReviews?.Select(o => o.Review);

        public virtual Location Location { get; set; }

        public virtual Region ServiceRegion { get; set; }

        [ForeignKey("ServiceId")]
        public virtual ICollection<Listing> Listings { get; set; }

        public virtual ICollection<OnlineStoreReview> OnlineStoreReviews { get; set; }

    }
}
