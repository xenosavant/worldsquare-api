using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Data.Account;
using Stellmart.Api.Services;
using Stellmart.Data.Account;
using Stellmart.Data.ViewModels;
using System.Net;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    [Route("api/[controller]")]
    public class AccountController : AuthorizedController
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountController(IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        /// <summary>
        /// Creating new account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route(template: "")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task Signup([FromBody]SignupRequest request)
        {
            await _accountService.SignupAsync(request);
        }

        /// <summary>
        /// Creating new account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route(template: "")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<SignupResponse> Signup()
        {
            return _mapper.Map<SignupResponse>(await _accountService.GetSignupResponseAsync());
        }
    }
}