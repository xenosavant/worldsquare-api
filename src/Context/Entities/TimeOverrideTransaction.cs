using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class TimeOverrideTransaction : PreTransaction
    {
        public DateTime MinimumTime { get; set; }
    }
}
