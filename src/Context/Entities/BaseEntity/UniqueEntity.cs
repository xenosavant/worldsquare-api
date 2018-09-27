using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.BaseEntity
{
    public class UniqueEntity : Entity<int>, IUniqueEntity
    {
        public Guid UniqueId { get; set; }
    }
}
