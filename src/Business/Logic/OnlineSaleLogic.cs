using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.OnlineSale;
using Stellmart.Api.Data.Enums;
using Stellmart.Api.Data.Contracts;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Context;
using Stellmart.Api.Context.Entities.Payment;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public class OnlineSaleLogic : IOnlineSaleLogic
    {
        private readonly ISignatureService _singatureService;
        private readonly IHorizonService _horizonService;
        private readonly IContractService _contractService;
        private readonly ICartDataManager _cartDataManager;
        private readonly IOnlineSaleDataManager _onlineSaleManager;
        private readonly IOnlineStoreDataManager _onlineStoreManager;
        private readonly IOrderDataManager _orderManager;
        private readonly IOrderItemDataManager _orderItemManager;
        private readonly IUserDataManager _userManager;
        private readonly IEncryptionService _encryptionService;
        private readonly IPaymentService _paymentService;

        public OnlineSaleLogic(
            ISignatureService signatureService, 
            IHorizonService horizonService,
            ICartDataManager cartDataManager,
            IOnlineSaleDataManager onlineSaleManager,
            IOrderDataManager orderManager,
            IContractService contractService,
            IOrderItemDataManager orderItemManager,
            IUserDataManager userManager,
            IOnlineStoreDataManager onlineStoreManager,
            IEncryptionService encryptionService,
            IPaymentService paymentService)
        {
            _singatureService = signatureService;
            _contractService = contractService;
            _horizonService = horizonService;
            _cartDataManager = cartDataManager;
            _onlineSaleManager = onlineSaleManager;
            _orderManager = orderManager;
            _orderItemManager = orderItemManager;
            _userManager = userManager;
            _onlineStoreManager = onlineStoreManager;
            _encryptionService = encryptionService;
            _paymentService = paymentService;
        }

        public async Task<OnlineSaleData> Checkout(ApplicationUser user, string password)
        {
            var cart = await _cartDataManager.GetAsync(user.Id, "LineItems.InventoryItem.Listing");

            // seperate cart items into groups by serveiceid
            var groupedByService = cart.LineItems.GroupBy(li => li.InventoryItem.Listing.ServiceId);

            var returnOrders = new List<Order>();
            // create a new online sale interaction for each one with an obligation
            foreach (var group in groupedByService)
            {
                var service = await _onlineStoreManager.GetById(group.Key, "User");
                var secret = _encryptionService.DecryptSecretKey(user.StellarEncryptedSecretKey, user.StellarSecretKeyIv, password);
                var now = DateTime.Now;
                var onlineSale = new OnlineSale() {
                    Obligations = new List<Obligation>()
                    {
                        new Obligation()
                        {
                            ServiceId = service.Id,
                            ProviderId = service.UserId,
                            RecipientId = user.Id,
                            InteracationId = 0,
                            FundingTimeLimit = now.AddDays(1),
                            ServiceInitiationTimeLimit = now.AddDays(3),
                            ServiceFulfillmentTimeLimit = now.AddDays(14),
                            ServiceReceiptTimeLimit = now.AddDays(17),
                            IntermediaryPhases = 1,
                            Fulfilled = false
                        }
                    }
                };

                var savedOnlineSale = await _onlineSaleManager.CreateAsync(onlineSale);
                var order = new Order()
                {
                    Sale = onlineSale,
                    PurchaserId = user.Id
                };

                var contracts = new List<Contract>();
                foreach (var item in group)
                {
                    var model = new ContractParameterModel()
                    {
                        SourceAccountSecret = new ContractSignatureModel()
                        {
                            Secret = secret
                        },
                        Obligation = onlineSale.Obligation,
                        Asset = new HorizonAssetModel()
                        {
                            AssetType = "native"
                        },
                        DestinationAccountId = service.User.StellarPublicKey,
                        SourceAccountId = user.StellarPublicKey
                    };

                    var contract = await _contractService.SetupContractAsync(model);
                    var orderItem = new OrderItem()
                    {
                        StoreId = service.Id,
                        InventoryItem = item.InventoryItem,
                        Contract = contract
                    };
                    order.Items.Add(orderItem);
                    onlineSale.Obligation.Contracts.Add(contract);
                }
                await _orderManager.CreateAsync(order);
                returnOrders.Add(order);
            }

            return new OnlineSaleData()
            {
                Orders = returnOrders,
                Success = true
            };
        }

        public async Task<PaymentResult> MakePayment(ApplicationUser user, List<OnlineSale> sales, string password, int paymentMethodId)
        {
            _paymentService.SetContracts(sales.SelectMany(s => s.Obligation.Contracts).ToList());
            _paymentService.SetUser(user);
            var secret = _encryptionService.DecryptSecretKey(user.StellarEncryptedSecretKey, user.StellarSecretKeyIv, password);
            _paymentService.SetSecret(secret);
            _paymentService.SetStrategy((IPaymentStrategy)user.GetPaymentMethod(paymentMethodId));
            return await _paymentService.MakePayment();
        }

        public async Task<PaymentResult> ValidatePayment(ApplicationUser user, List<OnlineSale> sales, string password, int paymentMethodId)
        {
            _paymentService.SetContracts(sales.SelectMany(s => s.Obligation.Contracts).ToList());
            _paymentService.SetUser(user);
            var secret = _encryptionService.DecryptSecretKey(user.StellarEncryptedSecretKey, user.StellarSecretKeyIv, password);
            _paymentService.SetSecret(secret);
            _paymentService.SetStrategy((IPaymentStrategy)user.GetPaymentMethod(paymentMethodId));
            return await _paymentService.ValidatePayment();
        }
    }
}
