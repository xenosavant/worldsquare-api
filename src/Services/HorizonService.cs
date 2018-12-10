using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.xdr;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;
using Asset = stellar_dotnet_sdk.Asset;
using Operation = stellar_dotnet_sdk.Operation;
using Signer = stellar_dotnet_sdk.Signer;
using TimeBounds = stellar_dotnet_sdk.TimeBounds;
using Transaction = stellar_dotnet_sdk.Transaction;

namespace Stellmart.Services
{
    public class HorizonService : IHorizonService
    {
        private readonly IHorizonServerManager _horizonServerManager;
        private readonly IOptions<HorizonSettings> _horizonSettings;
        private readonly IMapper _mapper;

        public HorizonService(IOptions<HorizonSettings> horizonSettings, IMapper mapper,
            IHorizonServerManager horizonServerManager)
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


        public async Task<string> GetAccountBalance(string accountPublicKey, HorizonAssetModel Asset)
        {
            var accountResponse = await _horizonServerManager.GetAccountAsync(accountPublicKey);
            var balances = accountResponse.Balances;

            if(Asset.AssetType.Equals("native")) {
                foreach (var balance in balances)
                {
                    if (!balance.AssetType.Equals("native")) continue;

                    return balance.BalanceString;
                }
            } else {
                foreach (var balance in balances)
                {
                    if (!balance.AssetCode.Equals(Asset.AssetCode)) continue;

                    if (!balance.AssetIssuer.AccountId.Equals(Asset.AssetIssuerPublicKey)) continue;

                    return balance.BalanceString;
                }
            }
            return null;
        }

        public Operation CreatePaymentOperation(string sourceAccountPublicKey, string destinationAccountPublicKey,
            HorizonAssetModel horizonAsset)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);

            if (horizonAsset.AssetType.Equals("native"))
            {
                Asset asset = new AssetTypeNative();
                var operation =
                    new PaymentOperation.Builder(KeyPair.FromAccountId(destinationAccountPublicKey), asset,
                            horizonAsset.Amount)
                        .SetSourceAccount(source)
                        .Build();
                return operation;
            }
            else
            {
                Asset asset = new AssetTypeCreditAlphaNum4(horizonAsset.AssetCode,
                        KeyPair.FromAccountId(horizonAsset.AssetIssuerPublicKey));
                var operation =
                    new PaymentOperation.Builder(KeyPair.FromAccountId(destinationAccountPublicKey), asset,
                            horizonAsset.Amount)
                        .SetSourceAccount(source)
                        .Build();
                return operation;
            }
        }

        public Operation SetOptionsWeightOperation(string sourceAccountPublicKey, HorizonAccountWeightModel weight)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);
            var operation = new SetOptionsOperation.Builder();

            if (weight.MasterWeight >= 0)
                operation.SetMasterKeyWeight(weight.MasterWeight);
            if (weight.LowThreshold >= 0)
                operation.SetLowThreshold(weight.LowThreshold);
            if (weight.MediumThreshold >= 0)
                operation.SetMediumThreshold(weight.MediumThreshold);
            if (weight.HighThreshold >= 0)
                operation.SetHighThreshold(weight.HighThreshold);

            /*BUG: Second signer is not getting added */
            if (weight.Signers != null)
                foreach (var signerAccount in weight.Signers)
                    operation.SetSigner(Signer.Ed25519PublicKey(KeyPair.FromAccountId(signerAccount.Signer)),
                        signerAccount.Weight);

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
            Asset asset = new AssetTypeCreditAlphaNum4(assetModel.AssetCode,
                        KeyPair.FromAccountId(assetModel.AssetIssuerPublicKey));

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

        public Operation CreateAccountOperation(string sourceAccountPublicKey, string destAccountPublicey,
            string amount)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);

            var dest = KeyPair.FromAccountId(destAccountPublicey);
            var operation = new CreateAccountOperation.Builder(dest, amount)
                .SetSourceAccount(source)
                .Build();

            return operation;
        }

        public async Task<string> CreateTransaction(string sourceAccountPublicKey, List<Operation> operations,
            HorizonTimeBoundModel time, long sequence)
        {
            var accountResponse = await _horizonServerManager.GetAccountAsync(sourceAccountPublicKey);

            Transaction.Builder transactionBuilder;

            if (sequence == 0)
                transactionBuilder =
                    new Transaction.Builder(new Account(accountResponse.KeyPair, accountResponse.SequenceNumber));
            else
                transactionBuilder = new Transaction.Builder(new Account(accountResponse.KeyPair, sequence));

            foreach (var operation in operations) transactionBuilder.AddOperation(operation);

            if (time != null) transactionBuilder.AddTimeBounds(new TimeBounds(time.MinTime, time.MaxTime));

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
            var keyPair = KeyPair.FromSecretSeed(secretKey);
            return keyPair.AccountId;
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

        public async Task<bool> PaymentTransaction(HorizonKeyPairModel Source, string DestinationPublicKey,
            HorizonAssetModel asset)
        {
            var operations = new List<Operation>();

            var paymentOperation = CreatePaymentOperation(DestinationPublicKey, Source.PublicKey, asset);
            operations.Add(paymentOperation);

            var xdrTransaction = await CreateTransaction(Source.PublicKey, operations, null, 0);

            var response = await SubmitTransaction(SignTransaction(Source, null, xdrTransaction));

            return response.IsSuccess();
        }

        private static Transaction ConvertXdrToTransaction(string transaction)
        {
            var bytes = Convert.FromBase64String(transaction);
            var transactionEnvelope = TransactionEnvelope.Decode(new XdrDataInputStream(bytes));
            return Transaction.FromEnvelopeXdr(transactionEnvelope);
        }
    }
}