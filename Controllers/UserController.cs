using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModel = Stellmart.Data.ViewModels;
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
        public async Task<IEnumerable<ViewModel.User>> Get()
        {
            return _mapper.Map<List<ViewModel.User>>(await _userLogic.GetAll());
        }

        // GET: api/user/get/5
        [HttpGet]
        [Route("{id:int}", Name = nameof(Get))]
        public async Task<ViewModel.User> Get(int id)
        {
            return _mapper.Map<ViewModel.User>(await _userLogic.Get(id));
        }
        
        // POST: api/user/signup
        [HttpPost]
        public async Task<ViewModel.User> Signup([FromBody]SignupRequest request)
        {
            return _mapper.Map<ViewModel.User>(await _userLogic.Signup(request));
        }
       
    }
}
