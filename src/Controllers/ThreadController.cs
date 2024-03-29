﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.Data.Thread;
using Stellmart.Api.Data.ViewModels;
using Stellmart.Api.Services.Interfaces;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ThreadController : BaseController
    {
        private readonly int UserId = 1;
        private readonly IMapper _mapper;
        private readonly IThreadDataManager _threadDataManager;
        private readonly IThreadLogic _threadLogic;
        private readonly ISecurityService _securityService;

        public ThreadController(IMapper mapper, 
            IThreadLogic threadLogic, 
            IThreadDataManager threadDataManager,
            ISecurityService securityService)
        {
            _mapper = mapper;
            _threadLogic = threadLogic;
            _threadDataManager = threadDataManager;
            _securityService = securityService;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<ThreadViewModel>>> Get([FromQuery] int? listingId,
            int? page,
            int? pageLength
        )
        {
            return _mapper.Map<List<ThreadViewModel>>(await _threadLogic.GetAsync(UserId, listingId, page, pageLength));
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetThread")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ThreadViewModel>> GetSingle(int id)
        {
            var thread = await _threadDataManager.GetById(id);
            if (thread == null)
            {
                return NotFound();
            }
            return _mapper.Map<ThreadViewModel>(thread);
        }


        [HttpPost]
        [Route("")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ThreadViewModel>> Post([FromBody] CreateThreadRequest request)
        {
            if (! (await _securityService.IsAllowedToPostListingThread(UserId, request.ListingId)))
            {
                return BadRequest();
            }
            var thread = await _threadDataManager.CreateAndSaveAsync(_mapper.Map<MessageThread>(request), UserId);
            return _mapper.Map<ThreadViewModel>(thread);
        }

        [HttpPatch]
        [Route("{id:int}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ThreadViewModel>> Patch(int id, [FromBody] PostMessageRequest request)
        {
            var thread = await _threadLogic.PostMessageToThread(_mapper.Map<Message>(request), id, UserId);
            return _mapper.Map<ThreadViewModel>(thread);
        }
    }
}