using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Region : Entity<int>
    {
       public string Name { get; set; }
        
       public string LocationId { get; set; }

       public virtual Location Location { get; set; }
    }
}
