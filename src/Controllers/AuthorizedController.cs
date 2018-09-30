using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Stellmart.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class AuthorizedController : BaseController
    {
        public int UserId
        {
            get
            {
                int.TryParse(User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out int id);
                return id;
            }
        }
    }
}