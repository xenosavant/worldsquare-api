using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context
{
    public class ChangeWeightsTransaction : PreTransaction
    {
        public DateTime MinimumTime { get; set; }
    }
}
