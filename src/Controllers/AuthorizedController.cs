using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Stellmart.Api.Controllers
{
    [Authorize]
    public class AuthorizedController : BaseController
    {

    }
}