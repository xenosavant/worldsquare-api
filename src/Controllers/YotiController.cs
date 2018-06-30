using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Data.Kyc;
using Stellmart.Api.Services;
using System.Net;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YotiController : ControllerBase
    {
        private readonly IKycService _kycService;

        public YotiController(IKycService kycService)
        {
            _kycService = kycService;
        }

        [HttpPost]
        [Route(template: "")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(YotiResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(YotiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(YotiResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(YotiResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody]YotiRequest request)
        {
            var test = await _kycService.GetUserProfileAsync(request.Token);

            if(test != null)
            {
                return Ok(new YotiResponse { IsVerified = true });
            }

            return Ok(new YotiResponse { IsVerified = false });
        }
    }
}