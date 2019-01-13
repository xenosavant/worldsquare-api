using Stellmart.Api.Context.Entities.ReadOnly;
using Stellmart.Api.Data.Enums;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Data.Payments;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.Payment
{
    public class ManagedXlmPayment : PaymentMethod, IPaymentStrategy
    {
        // user payment context as parameter
        public async Task<PaymentResult> ValidatePayment(PaymentContext context)
        {
            var sum = context.Contracts.Sum(c => c.FundingAmount);

            // TODO: we will have to convert here if the service is priced in 
            // non-native asset
            var assetModel = new HorizonAssetModel()
            {
                AssetType = "native",
                Amount = sum.ToString(),
                SourceAccountPublicKey = context.User.StellarPublicKey
            };
            
            var balance = await context.HorizonService.GetAccountBalanceAsync(assetModel);
            int.TryParse(balance, out int balanceInt);
            if (balanceInt > sum)
            {
                return PaymentResult.InsufficentFunds;
            }
            return PaymentResult.Completed;
        }

        public async Task<PaymentResult> InitiatePayment(PaymentContext context)
        {
            var success = true;
            foreach (var contract in context.Contracts)
            {
                var assetModel = new HorizonAssetModel()
                {
                    AssetType = "native",
                    Amount = contract.FundingAmount.ToString(),
                    SourceAccountPublicKey = context.User.StellarPublicKey
                };
                // calll fund contract here?
                var result = await context.HorizonService.PaymentTransaction(assetModel, context.SecretKey);
                if (!result)
                {
                    success = false;
                    contract.ContractStateId = (int)ContractStates.Error;
                }
                // add to context with data manager
            }
            if (!success)
            {
                return PaymentResult.Error;
            }
            return PaymentResult.Completed;
        }
    }
}
