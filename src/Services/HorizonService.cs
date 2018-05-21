using stellar_dotnetcore_sdk;
using Stellmart.Api.Services;
using Stellmart.Api.Data.Horizon;
using Microsoft.Extensions.Options;
using Stellmart.Api.Data.Settings;
using AutoMapper;

namespace Stellmart.Services
{
    public class HorizonService : IHorizonService
    {
        private readonly Server _server;
        private readonly IOptions<HorizonSettings> _horizonSettings;
        private readonly IMapper _mapper;

        public HorizonService(IOptions<HorizonSettings> horizonSettings, IMapper mapper)
        {
            _horizonSettings = horizonSettings;
            _mapper = mapper;

            if (_horizonSettings.Value.Server.Contains("testnet"))
            {
                Network.UseTestNetwork();
            }
            else
            {
                Network.UsePublicNetwork();
            }
            
            _server = new Server(_horizonSettings.Value.Server);
        }

        public void CreateAccount(HorizonKeyPairModel data)
        {
            var keypair = _mapper.Map<HorizonKeyPairModel>(KeyPair.Random());

            //do whatever next
        }
    }
}
