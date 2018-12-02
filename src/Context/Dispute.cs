using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context
{
    public class Dispute : Entity<int>
    {
        public int OrderId { get; set; }
        public int ThreadId { get; set; }
    }
}
