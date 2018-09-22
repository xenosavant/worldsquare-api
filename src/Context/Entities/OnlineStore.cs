using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Stellmart.Api.Context.Entities
{
    public class OnlineStore : Service
    {
        [Required]
        public bool Global { get; set; } 

        public int ServiceRegionId { get; set;  }

        [NotMapped]
        public IEnumerable<Review> Reviews => OnlineStoreReviews?.Select(o => o.Review);

        public virtual Region ServiceRegion { get; set; }

        [ForeignKey("ServiceId")]
        public virtual ICollection<Listing> Listings { get; set; }

        public virtual ICollection<OnlineStoreReview> OnlineStoreReviews { get; set; }

    }
}
