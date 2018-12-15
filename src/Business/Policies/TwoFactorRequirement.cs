using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Policies
{
    public class TwoFactorRequirement : IAuthorizationRequirement
    {
        // number of seconds allowed since last 2 factor auth for secure endpoints
        public readonly int ExpirationTime = 300;

        // whethere 2 factor is required for this endpoint
        public bool Required { get; private set; }

        public TwoFactorRequirement(bool required)
        {
            Required = required;
        }
    }
}
