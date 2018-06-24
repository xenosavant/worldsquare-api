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

        [HttpGet]
        [Route("create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(KycProfileModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(KycProfileModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(KycProfileModel), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Create([FromQuery]string token)
        {
            var test = await _kycService.GetUserProfileAsync(token);

            return Content(test.FirstName + ", " + test.LastName);
        }
    }
}