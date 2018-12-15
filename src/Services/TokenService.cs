using System.Collections.Generic;
using System.Threading.Tasks;
using stellar_dotnet_sdk;
using Stellmart.Api.Data.Enums;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Services.Interfaces;

namespace Stellmart.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IHorizonService _horizonService;

        public TokenService(IHorizonService horizonService)
        {
            _horizonService = horizonService;
        }

        public async Task<HorizonTokenModel> CreateAsset(string name, string limit)
        {
            var asset = new HorizonAssetModel {AssetCode = name, AssetType = "credit_alphanum4"};
            var token = new HorizonTokenModel {HorizonAssetModel = asset, MaxCoinLimit = limit};

            var issuer = _horizonService.CreateAccount();
            var distributor = _horizonService.CreateAccount();

            //TBD : Real network code is pending
            //Fund minimum XLM to create operations
            await _horizonService.FundTestAccountAsync(issuer.PublicKey);

            await _horizonService.FundTestAccountAsync(distributor.PublicKey);

            token.IssuerAccount = issuer;

            token.Distributor = distributor;

            token.HorizonAssetModel.AssetIssuerPublicKey = issuer.PublicKey;

            //Create trustline from Distributor to Issuer
            var operations = new List<Operation>();
            var trustOperation = _horizonService.ChangeTrustOperation(distributor.PublicKey, asset, limit);
            operations.Add(trustOperation);

            var xdrTransaction = await _horizonService.CreateTransaction(distributor.PublicKey, operations, time: null, sequence: 0);

            await _horizonService.SubmitTransaction(_horizonService.SignTransaction(distributor.SecretKey, xdrTransaction));

            token.State = CustomTokenState.CREATE_CUSTOM_TOKEN;

            return token;
        }

        public async Task<bool> MoveAssetToDistributor(HorizonTokenModel token)
        {
            if (token.State != CustomTokenState.CREATE_CUSTOM_TOKEN)
            {
                return false;
            }

            token.MaxCoinLimit = token.MaxCoinLimit;

            var horizonTokenModel = new HorizonTokenModel
                                    {
                                        HorizonAssetModel = new HorizonAssetModel
                                                            {
                                                                Amount = token.MaxCoinLimit,
                                                                AssetIssuerPublicKey = token.Distributor.PublicKey,
                                                                AccountPublicKey = token.Distributor.PublicKey
                                                            }
                                    };

            var result = await _horizonService.PaymentTransaction(horizonTokenModel);

            if (result)
            {
                token.State = CustomTokenState.MOVE_CUSTOM_TOKEN;
            }

            return result;
        }

        public async Task<bool> LockIssuer(HorizonTokenModel token)
        {
            if (token.State == CustomTokenState.MOVE_CUSTOM_TOKEN)
            {
                //Set threshold and weights of Issuer account as 0; so that no more coin can be minted.
                //All the coins should have been transferred to Distribution account by now.
                //Its the responsibility of the Distribution account to transfer the tokens to others.
                var weight = new HorizonAccountWeightModel {MasterWeight = 0};

                //Let the SignerSecret be null
                var operations = new List<Operation>();

                var setOptionsWeightOperation = _horizonService.SetOptionsWeightOperation(token.IssuerAccount.PublicKey, weight);
                operations.Add(setOptionsWeightOperation);

                var xdrTransaction = await _horizonService.CreateTransaction(token.IssuerAccount.PublicKey, operations, time: null, sequence: 0);

                await _horizonService.SubmitTransaction(_horizonService.SignTransaction(token.IssuerAccount.SecretKey, xdrTransaction));

                token.State = CustomTokenState.LOCK_CUSTOM_TOKEN;

                return true;
            }

            return false;
        }
    }
}