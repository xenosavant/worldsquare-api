using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Stellmart.Api.Data;
using System.Net;
using Stellmart.Api.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Stellmart.Api.Business.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISecurityLogic _securityLogic;

        public SecurityController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public string UserName
        {
            get
            {
                return User?.Identity.Name;
            }
        }

        // GET: api/security
        [HttpGet]
        public ActionResult<List<SecurityQuestionViewModel>> Get()
        {

            return Ok(_securityLogic.GetQuestions(UserName));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



    }
}
