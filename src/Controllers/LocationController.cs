using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Data;
using Stellmart.Api.Data.Account;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : AuthorizedController
    {
        private readonly ILocationLogic _locationLogic;

        public LocationController(ILocationLogic locationLogic)
        {
            _locationLogic = locationLogic;
        }

        /// <summary>
        /// Get shipping addresses
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(template: "user")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IReadOnlyCollection<LocationModel>> GetLocationsAsync()
        {
            return await _locationLogic.GetLocationsAsync(UserId);
        }

        /// <summary>
        /// Save shipping address
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(template: "")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task CreateAsync(LocationModel model)
        {
            await _locationLogic.CreateAsync(model, UserId);
        }
    }
}