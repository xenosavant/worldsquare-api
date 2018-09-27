using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Region : Entity<int>
    {
       public string LocationComponents { get; set; }

       public virtual OnlineStore OnlineStore { get; set; }
    }
}
