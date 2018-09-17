using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class DistributionCenter : Service
    {
        public bool Internal { get; set; }
    }
}
