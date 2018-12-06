using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Checkout;
using Stellmart.Api.Data.Enums;
using Stellmart.Api.Data.Contract;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public class CheckoutLogic : ICheckoutLogic
    {
        private readonly ISignatureService _singatureService;
        private readonly IHorizonService _horizonService;
        private readonly IContractService _contractService;
        private readonly ICartDataManager _cartDataManager;
        private readonly IOnlineSaleDataManager _onlineSaleManager;
        private readonly IOrderDataManager _orderManager;
        private readonly IOrderItemDataManager _orderItemManager;
        private readonly IUserDataManager _userManager;

        public CheckoutLogic(
            ISignatureService signatureService, 
            IHorizonService horizonService,
            ICartDataManager cartDataManager,
            IOnlineSaleDataManager onlineSaleManager,
            IOrderDataManager orderManager,
            IContractService contractService,
            IOrderItemDataManager orderItemManager,
            IUserDataManager userManager)
        {
            _singatureService = signatureService;
            _contractService = contractService;
            _horizonService = horizonService;
            _cartDataManager = cartDataManager;
            _onlineSaleManager = onlineSaleManager;
            _orderManager = orderManager;
            _orderItemManager = orderItemManager;
            _userManager = userManager;
        }

        public async Task<CheckoutData> ManagedCheckout(int userId, string password,
            int nativeCurrencyTypeId = (int)NativeCurrencyTypes.Default)
        {
            var user = _userManager.GetByIdAsync(userId);
            var cart = await _cartDataManager.GetAsync(userId, "LineItems.InventoryItem.Listing");
            var onlineSale = await _onlineSaleManager.CreateAsync();
            var order = new Order()
            {
                Sale = onlineSale,
                PurchaserId = userId
            };
            foreach (var item in cart.LineItems)
            {
                var orderItem = new OrderItem()
                {
                    StoreId = item.InventoryItem.Listing.ServiceId,
                    InventoryItem = item.InventoryItem,
                };
                // ToDo: Review me; SetupContractAsync needs param
                var model = new ContractParameterModel();
                orderItem.Contract = await _contractService.SetupContractAsync(model);
                // TODO: create contract

                //var model = new ContractParameterModel()
                //{
                //    ContractTypeId = (int)ContractTypes.OnlineSaleInternalShippingValidation,
                //    DestinationAccount = new 
                //    SourceAccount = // get this from currentUserAccount,
                //    Asset = new HorizonAssetModel()
                //    {
                //        IsNative = true,
                //        Amount = // get this from product total
                //    }
                //};
                //orderItem.Contract = await _contractService.FundContractAsync(orderItem.Contract, model);
                _orderItemManager.Update(orderItem);
            }

            await _orderManager.CreateAsync(order);

            return new CheckoutData()
            {
                Order = order,
                Success = true
            };
        }

    }
}
