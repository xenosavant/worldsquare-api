using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Data.ViewModels;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ThreadController
    {
        private readonly IMapper _mapper;
        private readonly IThreadDataManager _threadDataManager;
        private readonly IThreadLogic _threadLogic;

        public ThreadController(IMapper mapper, IThreadLogic threadLogic, IThreadDataManager threadDataManager)
        {
            _mapper = mapper;
            _threadLogic = threadLogic;
            _threadDataManager = threadDataManager;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ThreadViewModel>>> Get([FromQuery] int? listingId,
            int? page,
            int? pageLength
        )
        {
            //return await _threadLogic.GetAsync(listingId, page, pageLength));
            throw new NotImplementedException();
        }
    }
}