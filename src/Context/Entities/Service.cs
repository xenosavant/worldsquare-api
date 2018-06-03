using Stellmart.Api.Context.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public abstract class Service : EntityMaximum
    {
        string Name { get; set; }

        string Description { get; set; }

        string TagLine { get; set; }

        bool Verified { get; set; }

    }
}
