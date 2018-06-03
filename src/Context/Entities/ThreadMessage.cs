using Stellmart.Api.Context.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ThreadMessage : Entity<int>
    {
        int ThreadId { get; set; }

        int PosterId { get; set; }

        DateTime PostedOn { get; set; }

        string Message { get; set; }

    }
}
