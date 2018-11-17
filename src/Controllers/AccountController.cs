using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Data.Account;
using Stellmart.Api.Services.Interfaces;
using Stellmart.Data.Account;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    /// <summary>
    /// Account controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
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
        [HttpPost]
        [Route(template: "signup")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task SignupAsync([FromBody]SignupRequest request)
        {
            await _accountService.SignupAsync(_mapper.Map<ApplicationUserModel>(request), HttpContext);
        }

        /// <summary>
        /// Get security questions
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(template: "securityquestions")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IReadOnlyCollection<SecurityQuestionModel>> GetSecurityQuestionsAsync()
        {
            return await _accountService.GetSecurityQuestionsAsync();
        }

        /// <summary>
        /// Handle forgot password page postback
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(template: "forgotpassword")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ForgotPasswordRequest), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ForgotPasswordRequest), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ForgotPasswordRequest), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ForgotPasswordRequest), (int)HttpStatusCode.InternalServerError)]
        public async Task<bool> ForgotPasswordAsync([FromBody]ForgotPasswordRequest model)
        {
            return await _accountService.ForgotPassword(model);
        }

        /// <summary>
        /// Handle reset password page postback
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(template: "resetpassword")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResetPasswordRequest), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ResetPasswordRequest), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResetPasswordRequest), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResetPasswordRequest), (int)HttpStatusCode.InternalServerError)]
        public async Task<bool> ResetPasswordAsync([FromBody]ResetPasswordRequest model)
        {
            return await _accountService.ResetPassword(model);
        }

        /// <summary>
        /// Confirm email request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(template: "confirmemail")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResetPasswordRequest), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ResetPasswordRequest), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResetPasswordRequest), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResetPasswordRequest), (int)HttpStatusCode.InternalServerError)]
        public async Task<bool> ConfirmEmail([FromBody]ConfirmEmailRequest model)
        {
            return await _accountService.ConfirmEmail(model);
        }
    }
}