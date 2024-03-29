﻿using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Data;
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
        [ProducesResponseType(typeof(IReadOnlyCollection<LocationModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(IReadOnlyCollection<LocationModel>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IReadOnlyCollection<LocationModel>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IReadOnlyCollection<LocationModel>), (int)HttpStatusCode.InternalServerError)]
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
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.InternalServerError)]
        public async Task CreateAsync(LocationModel request)
        {
            await _locationLogic.CreateAsync(request, UserId);
        }

        /// <summary>
        /// Set default shipping address
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route(template: "setDefault")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.InternalServerError)]
        public async Task SetDefaultAsync(LocationModel request)
        {
            await _locationLogic.SetDefaultAsync(request, UserId);
        }

        /// <summary>
        /// Delete address
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route(template: "delete")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.InternalServerError)]
        public async Task DeleteAsync(LocationModel request)
        {
            await _locationLogic.DeleteAsync(request, UserId);
        }
    }
}