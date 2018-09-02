using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Managers;
using Stellmart.Api.Controllers;
using Stellmart.Data;
using Stellmart.Data.ViewModels;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    /// <summary>
    ///     Users
    /// </summary>
    [Route("api/[controller]")]
    public class UserController : AuthorizedController
    {
        private readonly IUserDataManager _userDataManager;
        private readonly IMapper _mapper;

        public UserController(IUserDataManager userDataManager, IMapper mapper)
        {
            _userDataManager = userDataManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(template: "")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationUserViewModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApplicationUserViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApplicationUserViewModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApplicationUserViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IEnumerable<ApplicationUserViewModel>> GetAll()
        {
            return _mapper.Map<List<ApplicationUserViewModel>>(await _userDataManager.GetAllAsync());
        }

        /// <summary>
        /// Get current logged user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(template: "me")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ApplicationUserViewModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ApplicationUserViewModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApplicationUserViewModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ApplicationUserViewModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApplicationUserViewModel> Get()
        {
            return _mapper.Map<ApplicationUserViewModel>(await _userDataManager.GetByIdAsync(UserId));
        }
        
        // POST: api/user/signup
        [HttpPost]
        public async Task<ApplicationUserViewModel> Signup([FromBody]SignupRequest request)
        {
            return _mapper.Map<ApplicationUserViewModel>(await _userDataManager.SignupAsync(request));
        }
       
    }
}
