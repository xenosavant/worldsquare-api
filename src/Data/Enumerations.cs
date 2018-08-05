using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Enumerations
{
    public enum HttpResponseCodes
    {
        Unauthorized = 401
    }

    public enum TwoFactorTypes
    {
        None = 0,
        Email = 1,
        Sms = 2,
        Totp = 3
    }
}
