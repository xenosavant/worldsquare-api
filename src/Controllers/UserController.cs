using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Data.ViewModels;
using AutoMapper;
using Stellmart.Business.Logic;
using Stellmart.Data;

namespace Stellmart.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic;

        private readonly IMapper _mapper;

        public UserController(IUserLogic userLogic, IMapper mapper)
        {
            _userLogic = userLogic;
            _mapper = mapper;
        }

        // GET: api/user/get
        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> Get()
        {
            return _mapper.Map<List<UserViewModel>>(await _userLogic.GetAll());
        }

        // GET: api/user/get/5
        [HttpGet]
        [Route("{id:int}", Name = nameof(Get))]
        public async Task<UserViewModel> Get(int id)
        {
            return _mapper.Map<UserViewModel>(await _userLogic.Get(id));
        }
        
        // POST: api/user/signup
        [HttpPost]
        public async Task<UserViewModel> Signup([FromBody]SignupRequest request)
        {
            return _mapper.Map<UserViewModel>(await _userLogic.Signup(request));
        }
       
    }
}
