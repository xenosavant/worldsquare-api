using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;

        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public int UserId
        {
            get
            {
                var principal = User as ClaimsPrincipal;
                Claim cl = principal.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
                Int32.TryParse(cl?.Value, out int id);
                return id;
            }
        }
    }
}
