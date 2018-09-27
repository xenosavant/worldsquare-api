using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context
{
    public abstract class Interaction : AuditableEntity<int>
    {
        int InteractionStateId { get; set; }

        public abstract void Update(IContractService contractService);

    }
}
