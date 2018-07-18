using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic;
using Stellmart.Data;
using Stellmart.Data.ViewModels;
using Stellmart.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic;

        private readonly IMapper _mapper;
        private readonly IHorizonService _horizonService;

        public UserController(IUserLogic userLogic, IMapper mapper, IHorizonService horizonService)
        {
            _userLogic = userLogic;
            _mapper = mapper;
            _horizonService = horizonService;
        }

        // GET: api/user/get
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ApplicationUserViewModel>> Get()
        {
            return _mapper.Map<List<ApplicationUserViewModel>>(await _userLogic.GetAllAsync());
        }

        // GET: api/user/get/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ApplicationUserViewModel> GetSingle(int id)
        {
            return _mapper.Map<ApplicationUserViewModel>(await _userLogic.GetByIdAsync(id));
        }
        
        // POST: api/user/signup
        [HttpPost]
        public async Task<ApplicationUserViewModel> Signup([FromBody]SignupRequest request)
        {
            return _mapper.Map<ApplicationUserViewModel>(await _userLogic.SignupAsync(request));
        }
       
    }
}
