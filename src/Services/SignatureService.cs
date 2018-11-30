using Microsoft.Extensions.Options;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Services
{
    public class SignatureService
    {
        private readonly HorizonKeyPairModel _worldSquareAccount;
        private readonly IHorizonService _horizonService;

        public SignatureService(IOptions<SignatureSettings> settings, IHorizonService horizonService)
        {
            _worldSquareAccount = new HorizonKeyPairModel()
            {
                PublicKey = settings.Value.MasterPublicKey,
                SecretKey = settings.Value.MasterSecrectKey
            };
            _horizonService = horizonService;
        }


    }
}
