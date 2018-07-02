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
    //[Authorize]
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
        [ProducesResponseType(typeof(KycResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(KycResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(KycResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(KycResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody]KycRequest request)
        {
            return Ok(new KycResponse { IsVerified = await _kycService.VerifyAsync(request) });
        }
    }
}