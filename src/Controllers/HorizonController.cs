using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.ViewModels.Horizon;
using Stellmart.Services;
using System.Threading.Tasks;

namespace Stellmart.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class HorizonController : Controller
    {
        private readonly IHorizonService _horizonService;
        private readonly IMapper _mapper;

        public HorizonController(IHorizonService horizonService, IMapper mapper)
        {
            _horizonService = horizonService;
            _mapper = mapper;
        }

        // GET: api/user/get
        [HttpGet]
        public async Task<ActionResult> CreateAccount()
        {
            return Ok(_mapper.Map<HorizonKeyPairViewModel>(_horizonService.CreateAccount()));
        }

        [HttpGet]
        [Route("{publicKey}")]
        public async Task<ActionResult> FundTestAccount(string publicKey)
        {
            return Ok(_mapper.Map<HorizonFundTestAccountViewModel>(await _horizonService.FundTestAccount(publicKey)));
        }


        
    }
}