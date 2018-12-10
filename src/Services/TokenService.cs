using stellar_dotnet_sdk;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Services
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
            var asset = new HorizonAssetModel
            {
                AssetCode = name, AssetType = "credit_alphanum4",
            };
            var Token = new HorizonTokenModel
            {
                Asset = asset, MaxCoinLimit = limit,
            };

            var issuer = _horizonService.CreateAccount();
            var distributor = _horizonService.CreateAccount();

            //TBD : Real network code is pending
            //Fund minimum XLM to create operations
            await _horizonService.FundTestAccountAsync(issuer.PublicKey);

            await _horizonService.FundTestAccountAsync(distributor.PublicKey);

            Token.IssuerAccount = issuer;

            Token.Distributor = distributor;

            Token.Asset.AssetIssuerPublicKey = issuer.PublicKey;

            //Create trustline from Distributor to Issuer
            var operations = new List<Operation>();
            var trustOperation = _horizonService.ChangeTrustOperation(distributor.PublicKey, asset, limit);
            operations.Add(trustOperation);

            var xdrTransaction = await _horizonService.CreateTransaction(distributor.PublicKey, operations, null, 0);

            await _horizonService.SubmitTransaction(_horizonService.SignTransaction(distributor, null, xdrTransaction));

            Token.State = CustomTokenState.CreateCustomToken;
            return Token;
        }

        public async Task<bool> MoveAssetToDistributor(HorizonTokenModel Token)
        {
            if (Token.State != CustomTokenState.CreateCustomToken) return false;

            Token.Amount = Token.MaxCoinLimit;
            var asset = new HorizonAssetModel {
                Amount = Token.MaxCoinLimit,
            };

            var result = await _horizonService.PaymentTransaction(Token.IssuerAccount, Token.Distributor.PublicKey, asset);
            if (result)
                Token.State = CustomTokenState.MoveCustomToken;

            return result;
        }

        public async Task<bool> LockIssuer(HorizonTokenModel Token)
        {
            if (Token.State == CustomTokenState.MoveCustomToken)
            {
                //Set threshold and weights of Issuer account as 0; so that no more coin can be minted.
                //All the coins should have been transferred to Distribution account by now.
                //Its the responsibility of the Distribution account to transfer the tokens to others.
                var weight = new HorizonAccountWeightModel
                {
                    MasterWeight = 0
                };

                //Let the SignerSecret be null
                var operations = new List<Operation>();

                var setOptionsWeightOperation = _horizonService.SetOptionsWeightOperation(Token.IssuerAccount.PublicKey, weight);
                operations.Add(setOptionsWeightOperation);

                var xdrTransaction = await _horizonService.CreateTransaction(Token.IssuerAccount.PublicKey, operations, null, 0);

                await _horizonService.SubmitTransaction(_horizonService.SignTransaction(Token.IssuerAccount, null, xdrTransaction));

                Token.State = CustomTokenState.LockCustomToken;

                return true;
            }

            return false;
        }
    }
}