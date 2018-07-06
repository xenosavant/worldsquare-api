using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class RewardsLevel : LookupData
    {
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
