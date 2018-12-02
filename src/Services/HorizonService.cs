using AutoMapper;
using Microsoft.Extensions.Options;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.xdr;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Stellmart.Api.Business.Managers.Interfaces;
using Asset = stellar_dotnet_sdk.Asset;
using Operation = stellar_dotnet_sdk.Operation;
using Signer = stellar_dotnet_sdk.Signer;
using TimeBounds = stellar_dotnet_sdk.TimeBounds;
using Transaction = stellar_dotnet_sdk.Transaction;

namespace Stellmart.Services
{
    public class HorizonService : IHorizonService
    {
        private readonly IOptions<HorizonSettings> _horizonSettings;
        private readonly IMapper _mapper;
        private readonly IHorizonServerManager _horizonServerManager;

        public HorizonService(IOptions<HorizonSettings> horizonSettings, IMapper mapper, IHorizonServerManager horizonServerManager)
        {
            _horizonSettings = horizonSettings;
            _mapper = mapper;
            _horizonServerManager = horizonServerManager;

            if (_horizonSettings.Value.Server.Contains("testnet"))
                Network.UseTestNetwork();
            else
                Network.UsePublicNetwork();
        }

        public HorizonKeyPairModel CreateAccount()
        {
            return _horizonServerManager.CreateAccount();
        }

        public async Task<HorizonFundTestAccountModel> FundTestAccountAsync(string publicKey)
        {
            // fund test acc
            await _horizonServerManager.FundTestAccountAsync(publicKey);

            //See our newly created account.
            var accountResponse = await _horizonServerManager.GetAccountAsync(publicKey);
            return _mapper.Map<HorizonFundTestAccountModel>(accountResponse);
        }

        public async Task<long> GetSequenceNumber(string publicKey)
        {
            var accountResponse = await _horizonServerManager.GetAccountAsync(publicKey);
            return accountResponse.SequenceNumber;
        }
        //Native balance example ("GAMUNY3XR53RJFUIIZDLKJFSLXAX4EJRGGPO7SXNNNR2PUGH2JSZXKKI", "native", null)
        public async Task<string> GetAccountBalance(string AccountPublicKey, string AssetType,
                string AssetIssuerPublicKey)
        {
            var accountResponse = await _horizonServerManager.GetAccountAsync(AccountPublicKey);
            var balances = accountResponse.Balances;
            foreach(Balance balance in balances)
            {
                if(balance.AssetType.Equals(AssetType)) {
                    if(AssetType.Equals("native"))
                        return balance.BalanceString;
                    else if(balance.Equals(AssetIssuerPublicKey))
                        return balance.BalanceString;
                    else return null;
                }
            }
            return null;
        }
        public Operation CreatePaymentOperation(string sourceAccountPublicKey, string destinationAccountPublicKey,
            HorizonAssetModel horizonAsset)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);

            if(horizonAsset.IsNative) {
                Asset asset = new AssetTypeNative();
                var operation =
                new PaymentOperation.Builder(KeyPair.FromAccountId(destinationAccountPublicKey), asset, horizonAsset.Amount)
                    .SetSourceAccount(source)
                    .Build();
                return operation;
            } else {
                Asset asset = new AssetTypeCreditAlphaNum4(horizonAsset.Code, horizonAsset.Issuer);
                var operation =
                new PaymentOperation.Builder(KeyPair.FromAccountId(destinationAccountPublicKey), asset, horizonAsset.Amount)
                    .SetSourceAccount(source)
                    .Build();
                return operation;
            }
        }

        public Operation SetOptionsWeightOperation(string sourceAccountPublicKey, HorizonAccountWeightModel weight)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);
            var operation = new SetOptionsOperation.Builder();

            if(weight.MasterWeight >= 0)
                operation.SetMasterKeyWeight(weight.MasterWeight);
            if(weight.LowThreshold >= 0)
                operation.SetLowThreshold(weight.LowThreshold);
            if(weight.MediumThreshold >= 0)
                operation.SetMediumThreshold(weight.MediumThreshold);
            if(weight.HighThreshold >= 0)
                operation.SetHighThreshold(weight.HighThreshold);

            /*BUG: Second signer is not getting added */
            if(weight.Signers != null) {
                foreach (var signerAccount in weight.Signers)
                {
                    operation.SetSigner(Signer.Ed25519PublicKey(KeyPair.FromAccountId(signerAccount.Signer)), signerAccount.Weight);
                }
            }

            if (weight.SignerSecret != null)
            {
                var hash = Util.Hash(Encoding.UTF8.GetBytes(weight.SignerSecret.Secret));
                operation.SetSigner(Signer.Sha256Hash(hash), weight.SignerSecret.Weight);
            }

            operation.SetSourceAccount(source);
            return operation.Build();
        }
        public Operation SetOptionsSingleSignerOperation(string secondSignerAccountPublicKey)
        {
            var source = KeyPair.FromAccountId(secondSignerAccountPublicKey);
            var operation = new SetOptionsOperation.Builder();

            operation.SetSourceAccount(source);
            return operation.Build();
        }
        public Operation CreateAccountMergeOperation(string sourceAccountPublicKey, string destAccountPublicKey)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);

            var operation = new AccountMergeOperation.Builder(KeyPair.FromAccountId(destAccountPublicKey))
                .SetSourceAccount(source)
                .Build();

            return operation;
        }

        public Operation ChangeTrustOperation(string sourceAccountPublicKey, HorizonAssetModel assetModel, string limit)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);
            Asset asset = new AssetTypeCreditAlphaNum4(assetModel.Code, assetModel.Issuer);

            var operation = new ChangeTrustOperation.Builder(asset, limit)
                .SetSourceAccount(source)
                .Build();

            return operation;
        }

        public Operation BumpSequenceOperation(string sourceAccountPublicKey, long nextSequence)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);

            var operation = new BumpSequenceOperation.Builder(nextSequence)
                .SetSourceAccount(source)
                .Build();

            return operation;
        }

        private Transaction ConvertXdrToTransaction(string transaction)
        {
            var bytes = Convert.FromBase64String(transaction);
            var transactionEnvelope = TransactionEnvelope.Decode(new XdrDataInputStream(bytes));
            return Transaction.FromEnvelopeXdr(transactionEnvelope);
        }

        public async Task<string> CreateTransaction(string sourceAccountPublicKey, List<Operation> operations, HorizonTimeBoundModel time, long sequence)
        {
            var accountResponse = await _horizonServerManager.GetAccountAsync(sourceAccountPublicKey);

            Transaction.Builder transactionBuilder;

            if (sequence == 0)
            {
                transactionBuilder = new Transaction.Builder(new Account(accountResponse.KeyPair, accountResponse.SequenceNumber));
            }
            else
            {
                transactionBuilder = new Transaction.Builder(new Account(accountResponse.KeyPair, sequence));
            }

            foreach (var operation in operations)
            {
                transactionBuilder.AddOperation(operation);
            }

            if (time != null)
            {
                transactionBuilder.AddTimeBounds(new TimeBounds(time.MinTime, time.MaxTime));
            }

            var transaction = transactionBuilder.Build();

            return transaction.ToUnsignedEnvelopeXdrBase64();
        }

        public string SignTransaction(HorizonKeyPairModel account, string secretKey, string xdrTransaction)
        {
            var transaction = ConvertXdrToTransaction(xdrTransaction);
            var usableSecretSeed = KeyPair.FromSecretSeed(secretKey ?? account.SecretKey);

            transaction.Sign(usableSecretSeed);
            return transaction.ToEnvelopeXdrBase64();
        }

        public async Task<SubmitTransactionResponse> SubmitTransaction(string xdrTransaction)
        {
            var transaction = ConvertXdrToTransaction(xdrTransaction);

            return await _horizonServerManager.SubmitTransaction(transaction);
        }

        public string GetPublicKey(string secretKey)
        {
            var keypair = KeyPair.FromSecretSeed(secretKey);
            return keypair.AccountId;
        }

        public int GetSignatureCount(string xdrTransaction)
        {
            var transaction = ConvertXdrToTransaction(xdrTransaction);
            return transaction.Signatures.Count;
        }

        public string SignatureHash(string xdrTransaction, int index)
        {
            var transaction = ConvertXdrToTransaction(xdrTransaction);
            return Encoding.UTF8.GetString(transaction.Signatures[index].Signature.InnerValue);
        }

        public async Task<HorizonAssetModel> CreateAsset(string name, string limit)
        {
            var asset = new HorizonAssetModel
            {
                IsNative = false, MaxCoinLimit = limit, Code = name
            };

            var issuer = CreateAccount();
            var distributor = CreateAccount();

            //TBD : Real network code is pending
            //Fund minimum XLM to create operations
            await FundTestAccountAsync(issuer.PublicKey);
            await FundTestAccountAsync(distributor.PublicKey);
            asset.IssuerAccount = issuer;
            asset.Distributor = distributor;
            asset.Issuer = KeyPair.FromAccountId(issuer.PublicKey);

            //Create trustline from Distributor to Issuer
            var operations = new List<Operation>();
            var trustOperation = ChangeTrustOperation(distributor.PublicKey, asset, limit);
            operations.Add(trustOperation);

            var xdrTransaction = await CreateTransaction(distributor.PublicKey, operations, null, 0);
            await SubmitTransaction(SignTransaction(distributor, null, xdrTransaction));
            asset.State = CustomTokenState.CreateCustomToken;
            return asset;
        }

        public async Task<bool> MoveAsset(HorizonAssetModel asset)
        {
            if (asset.State != CustomTokenState.CreateCustomToken)
            {
                return false;
            }
            //TBD: bad coding, create different class for new asset created by us
            asset.Amount = asset.MaxCoinLimit;

            var result = await PaymentTransaction(asset.IssuerAccount, asset.Distributor.PublicKey, asset);
            if(result)
                asset.State = CustomTokenState.MoveCustomToken;

            return result;
        }

        public async Task<bool> LockAsset(HorizonAssetModel asset)
        {
            if (asset.State == CustomTokenState.MoveCustomToken)
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
                var setOptionsWeightOperation = SetOptionsWeightOperation(asset.IssuerAccount.PublicKey, weight);
                operations.Add(setOptionsWeightOperation);

                var xdrTransaction = await CreateTransaction(asset.IssuerAccount.PublicKey, operations, null, 0);
                await SubmitTransaction(SignTransaction(asset.IssuerAccount, null, xdrTransaction));
                asset.State = CustomTokenState.LockCustomToken;
                return true;
            }

            return false;
        }
        public async Task<bool> PaymentTransaction(HorizonKeyPairModel Source, string DestinationPublicKey,
                    HorizonAssetModel asset)
        {
            var operations = new List<Operation>();

            var paymentOperation = CreatePaymentOperation(DestinationPublicKey, Source.PublicKey, asset);
            operations.Add(paymentOperation);

            var xdrTransaction = await CreateTransaction(Source.PublicKey, operations, null, 0);

            var response = await SubmitTransaction(SignTransaction(Source, null, xdrTransaction));
            if (response.IsSuccess())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}