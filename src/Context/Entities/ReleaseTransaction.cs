using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities
{
    public class ReleaseTransaction : PreTransaction
    {
        public DateTime MinimumTime { get; set; }

        public DateTime MaximumTime { get; set; }
    }
}
