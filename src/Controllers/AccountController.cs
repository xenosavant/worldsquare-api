﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Managers.Interfaces;
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
        [Route(template: "signup")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task Signup([FromBody]SignupRequest request)
        {
            await _accountService.SignupAsync(_mapper.Map<ApplicationUserModel>(request));
        }

        /// <summary>
        /// Creating new account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route(template: "securityquestions")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(SignupResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IReadOnlyCollection<SecurityQuestionModel>> GetSecurityQuestions()
        {
            return await _accountService.GetSecurityQuestionsAsync();
        }
    }
}