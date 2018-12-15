using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Policies
{
    public class TwoFactorHandler : AuthorizationHandler<TwoFactorRequirement>, IAuthorizationHandler
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            TwoFactorRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "2fa_type"))
            {
                var authTime = GetDateFromEpoch(Convert.ToInt32(context.User.FindFirst(c => c.Type == "auth_time").Value));

                if (requirement.Required)
                {
                    if (context.User.HasClaim(c => c.Type == "2fa_time"))
                    {
                        var twoFactorTime = GetDateFromEpoch(Convert.ToInt32(context.User.FindFirst(c => c.Type == "2fa_time").Value));
                        var maxTime = twoFactorTime.AddSeconds(requirement.ExpirationTime);
                        if (VerifyTwoFactor(context, twoFactorTime, authTime, maxTime))
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
                else
                {
                    if (context.User.FindFirst(c => c.Type == "2fa_type").Value == "0")
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        var twoFactorTime = GetDateFromEpoch(Convert.ToInt32(context.User.FindFirst(c => c.Type == "2fa_time").Value));
                        if (VerifyTwoFactor(context, twoFactorTime, authTime))
                        {
                            context.Succeed(requirement);
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }

        private bool VerifyTwoFactor(AuthorizationHandlerContext context, DateTime twoFactorTime, DateTime minTime, DateTime? maxTime = null)
        {
            return (twoFactorTime > minTime) && (maxTime != null ?
                DateTime.UtcNow < maxTime : true);
        }

        private DateTime GetDateFromEpoch(int epoch)
        {
            return new DateTime(1970, 1, 1).AddSeconds(epoch);
        }
    }
}
