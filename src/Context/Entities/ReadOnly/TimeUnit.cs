using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class TimeUnit : LookupData
    {
        public virtual ICollection<PricePerTime> PricePerTimes { get; set; }
    }
}
