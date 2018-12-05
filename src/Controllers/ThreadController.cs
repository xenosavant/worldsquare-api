using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ThreadController
    {
        private readonly IMapper _mapper;
        private readonly IThreadLogic _threadLogic;
        private readonly IThreadDataManager _threadDataManager;

        public ThreadController(
         IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ThreadViewModel>>> Get([FromQuery]
           int? listingId,
           int? page,
           int? pageLength
       )
        {
            //return await _threadLogic.GetAsync(listingId, page, pageLength));
            throw new NotImplementedException();
        }


    }
}
