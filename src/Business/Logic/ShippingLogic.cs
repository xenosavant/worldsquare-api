using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Data.Contract;

namespace Stellmart.Api.Business.Logic
{
    public class ShippingLogic : IShippingLogic
    {
        private readonly IContractService _contractService;
        private readonly IShipmentTrackerDataManager _trackerMamager;
        private readonly ISignatureDataManager _signatureManager;

        public ShippingLogic(IContractService contractService, 
            IShipmentTrackerDataManager trackerMamager,
            ISignatureDataManager signatureManager)
        {
            _contractService = contractService;
            _trackerMamager = trackerMamager;
            _signatureManager = signatureManager;
        }

        public async Task<bool> MarkShipmentDelivered(string trackingId)
        {
            var tracker = await _trackerMamager.GetAsync(trackingId);
            var signature = tracker.Signature;
            return await _contractService.SignContract(
                new ContractSignatureModel()
                {
                    Secret = tracker.SecretSigningKey,
                    Signature = signature
                });
        }
    }
}
