using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Services.Interfaces;
using Stellmart.Api.ViewModels.Horizon;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers.Helpers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class HorizonController: ControllerBase
    {
        private readonly IHorizonService _horizonService;
        private readonly IMapper _mapper;

        public HorizonController(IHorizonService horizonService, IMapper mapper)
        {
            _horizonService = horizonService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return Ok(_mapper.Map<HorizonKeyPairViewModel>(_horizonService.CreateAccount()));
        }

        [HttpGet]
        [Route("{publicKey}")]
        public async Task<ActionResult> FundTestAccount(string publicKey)
        {
            return Ok(_mapper.Map<HorizonFundTestAccountViewModel>(await _horizonService.FundTestAccountAsync(publicKey)));
        }


        
    }
}