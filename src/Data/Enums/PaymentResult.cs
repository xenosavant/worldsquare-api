using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Enums
{
    public enum PaymentResult
    {
        Completed = 1,
        Processing = 2,
        InsufficentFunds = 3,
        Error = 4
    }
}
