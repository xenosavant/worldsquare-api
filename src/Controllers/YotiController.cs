using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Data.Kyc;
using Stellmart.Api.Services;
using Stellmart.Api.Services.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    /// <summary>
    ///     Yoti kyc
    /// </summary>
    [Route("api/[controller]")]
    public class YotiController : AuthorizedController
    {
        private readonly IKycService _kycService;

        public YotiController(IKycService kycService)
        {
            _kycService = kycService;
        }

        /// <summary>
        /// Verification of customer and storing customer data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(template: "")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(KycResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(KycResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(KycResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(KycResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Create([FromBody]KycRequest request)
        {
            return Ok(new KycResponse { IsVerified = await _kycService.VerifyAsync(request, UserId) });
        }
    }
}