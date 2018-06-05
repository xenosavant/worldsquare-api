using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class MessageThread : EntityMaximum
    {
        int InitiatorId { get; set; }

        public virtual ApplicationUser Initiator { get; set; }
    }
}
