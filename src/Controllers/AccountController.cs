using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Managers;
using Stellmart.Api.Data.Account;
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
        private readonly IUserDataManager _userDataManager;
        private readonly IMapper _mapper;

        public AccountController(IUserDataManager userDataManager, IMapper mapper)
        {
            this._userDataManager = userDataManager;
            this._mapper = mapper;
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
        public async Task<ApplicationUserViewModel> Signup([FromBody]SignupRequest request)
        {
            return _mapper.Map<ApplicationUserViewModel>(await _userDataManager.SignupAsync(request));
        }
    }
}