using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Cart : Entity<int>
    {
        public int UserId { get; set; }

        public virtual ICollection<LineItem> LineItems { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
