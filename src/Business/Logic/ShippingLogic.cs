using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contracts;
using Stellmart.Api.Data.Shipping;
using Stellmart.Api.Services.Interfaces;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public class ShippingLogic : IShippingLogic
    {
        private readonly IContractService _contractService;
        private readonly ISecretKeyDataManager _secretKeyManager;
        private readonly IShipmentTrackerDataManager _trackerManager;
        private readonly ISignatureDataManager _signatureManager;
        private readonly IProductShipmentDataManager _shipmentDataManager;
        private readonly IShippingService _shippingService;
        private readonly IUserDataManager _userDataManager;
        private readonly IEncryptionService _encryptionService;
        private readonly IOrderDataManager _orderDataManager;

        public ShippingLogic(IContractService contractService,
            IShipmentTrackerDataManager trackerManager,
            ISignatureDataManager signatureManager,
            IProductShipmentDataManager shipmentDataManager,
            IShippingService shippingService,
            IEncryptionService encryptionService,
            ISecretKeyDataManager secretKeyManager,
            IUserDataManager userDataManager,
            IOrderDataManager orderDataManager)
        {
            _secretKeyManager = secretKeyManager;
            _shipmentDataManager = shipmentDataManager;
            _contractService = contractService;
            _trackerManager = trackerManager;
            _encryptionService = encryptionService;
            _signatureManager = signatureManager;
            _shippingService = shippingService;
            _userDataManager = userDataManager;
            _orderDataManager = orderDataManager;
        }

        public async Task<ProductShipment> CreateAsync(ShipPackageData data)
        {
            var shipment = await _shipmentDataManager.CreateAsync(data);

            if (data.EasyPostTrackerId != null)
            {
                var order = await _orderDataManager.GetOrder(data.OrderId, "Sale.Obligations");
                var obligationId = order.Sale.Obligation.Id;
                var secretKey = await _secretKeyManager.GetSecretKeyByObligationId(obligationId);
                var user = await _userDataManager.GetByIdAsync(data.CurrentUserId);
                // TODO: we should probably verify this key is correct before we attempt to sign
                var stellarKey = _encryptionService.DecryptSecretKey(user.StellarEncryptedSecretKey, user.StellarSecretKeyIv, data.Password);
                foreach (var item in shipment.Items)
                {
                    var signature = await _signatureManager.GetUserSignatureAsync(
                        new GetSignatureModel()
                        {
                            UserId = data.CurrentUserId,
                            ContractId = item.Contract.Id,
                            PhaseNumber = item.Contract.CurrentSequenceNumber - item.Contract.BaseSequenceNumber
                        });
                    var success = _contractService.SignContract(
                        new ContractSignatureModel()
                        {
                            Signature = signature,
                            Secret = stellarKey
                        });
                    var tracker = _trackerManager.CreateAsync(secretKey.Key, data.EasyPostTrackerId, signature.Id);
                }
            }
            else
            {
                // TODO: in v2 set up internal service delivery contract
            }
            return shipment;
        }

        public async Task<bool> MarkShipmentDelivered(string trackingId)
        {
            // Get all item trackers with the id
            var tracker = await _trackerManager.GetAsync(trackingId);
            var signature = tracker.Signature;
            return _contractService.SignContract(
                new ContractSignatureModel()
                {
                    Secret = tracker.SecretSigningKey,
                    Signature = signature
                });
        }
    }
}
