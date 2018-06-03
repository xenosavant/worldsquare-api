using Stellmart.Api.Context.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class Region : Entity<int>
    {
       string Name { get; set; }
        
       string LocationComponentId { get; set; }
    }
}
