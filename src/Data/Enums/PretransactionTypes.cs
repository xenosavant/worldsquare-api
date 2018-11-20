using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Enums
{
    public enum PretransactionTypes
    {
        BumpSequence = 1,
        Release = 2,
        Dispute = 3,
        Refund = 4,
        TimeOverride = 5
    }
}
