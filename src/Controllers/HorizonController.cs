using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.ViewModels.Horizon;
using Stellmart.Services;
using Stellmart.Services.Interfaces;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers.Helpers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class HorizonController : BaseController
    {
        private readonly IHorizonService _horizonService;

        public HorizonController(IHorizonService horizonService, IMapper mapper) : base(mapper)
        {
            _horizonService = horizonService;
        }

        [HttpGet]
        public async Task<ActionResult> CreateAccount()
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