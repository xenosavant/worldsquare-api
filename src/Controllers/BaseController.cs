using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
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
