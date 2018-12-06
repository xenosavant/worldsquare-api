using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Review : AuditableEntity<int>
    {
        public int ServiceId { get; set; }
        public int? ListingId { get; set; }
        public int Stars { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public virtual ApplicationUser Reviewer { get; set; }
        public virtual Service Service { get; set; }
        public virtual Listing Listing { get; set; }
    }
}
