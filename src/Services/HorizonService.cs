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
using Asset = stellar_dotnet_sdk.Asset;
using Operation = stellar_dotnet_sdk.Operation;
using Signer = stellar_dotnet_sdk.Signer;
using TimeBounds = stellar_dotnet_sdk.TimeBounds;
using Transaction = stellar_dotnet_sdk.Transaction;

namespace Stellmart.Services
{
    public class HorizonService : IHorizonService
    {
        private readonly Server _server;
        private readonly IOptions<HorizonSettings> _horizonSettings;
        private readonly IMapper _mapper;

        public HorizonService(IOptions<HorizonSettings> horizonSettings, IMapper mapper, Server server)
        {
            _horizonSettings = horizonSettings;
            _mapper = mapper;
            _server = server;

            if (_horizonSettings.Value.Server.Contains("testnet"))
                Network.UseTestNetwork();
            else
                Network.UsePublicNetwork();
        }

        public HorizonKeyPairModel CreateAccount()
        {
            return _mapper.Map<HorizonKeyPairModel>(KeyPair.Random());
        }

        public async Task<HorizonFundTestAccountModel> FundTestAccountAsync(string publicKey)
        {
            // fund test acc
            await Server.HttpClient.GetAsync($"friendbot?addr={publicKey}");

            //See our newly created account.
            return _mapper.Map<HorizonFundTestAccountModel>(
                await _server.Accounts.Account(KeyPair.FromAccountId(publicKey)));
        }

        public async Task<long> GetSequenceNumber(string publicKey)
        {
            var accountRes = await _server.Accounts.Account(KeyPair.FromAccountId(publicKey));
            return accountRes.SequenceNumber;
        }

        public Operation CreatePaymentOperation(string sourceAccountPublicKey, string destinationAccountPublicKey,
            string amount)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);
            Asset native = new AssetTypeNative();

            var operation =
                new PaymentOperation.Builder(KeyPair.FromAccountId(destinationAccountPublicKey), native, amount)
                    .SetSourceAccount(source)
                    .Build();

            return operation;
        }

        public Operation SetOptionsOperation(string sourceAccountPublicKey, HorizonAccountWeightModel weights)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);
            var operation = new SetOptionsOperation.Builder();

            operation.SetMasterKeyWeight(weights.MasterWeight);
            operation.SetLowThreshold(weights.LowThreshold);
            operation.SetMediumThreshold(weights.MediumThreshold);
            operation.SetHighThreshold(weights.HighThreshold);

            /*BUG: Second signer is not getting added */
            foreach (var signerAccount in weights.Signers)
            {
                operation.SetSigner(Signer.Ed25519PublicKey(KeyPair.FromAccountId(signerAccount.Signer)), signerAccount.Weight);
            }

            if (weights.SignerSecret != null)
            {
                var hash = Util.Hash(Encoding.UTF8.GetBytes(weights.SignerSecret.Secret));
                operation.SetSigner(Signer.Sha256Hash(hash), weights.SignerSecret.Weight);
            }

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
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);
            var accountRes = await _server.Accounts.Account(source);

            Transaction.Builder transactionBuilder;

            if (sequence == 0)
            {
                transactionBuilder = new Transaction.Builder(new Account(source, accountRes.SequenceNumber));
            }
            else
            {
                transactionBuilder = new Transaction.Builder(new Account(source, sequence));
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

            return await _server.SubmitTransaction(transaction);
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

        public async Task<int> MoveAsset(HorizonAssetModel asset)
        {
            if (asset.State != CustomTokenState.CreateCustomToken)
            {
                return -1;
            }

            var operations = new List<Operation>();

            //TBD: PaymentOps supports only native currency XLM.
            //Add support for custom token transfer.
            var paymentOperation = CreatePaymentOperation(asset.IssuerAccount.PublicKey, asset.Distributor.PublicKey, asset.MaxCoinLimit);
            operations.Add(paymentOperation);

            var xdrTransaction = await CreateTransaction(asset.IssuerAccount.PublicKey, operations, null, 0);
            await SubmitTransaction(SignTransaction(asset.IssuerAccount, null, xdrTransaction));
            asset.State = CustomTokenState.MoveCustomToken;
            return 0;

        }

        public async Task<int> LockAsset(HorizonAssetModel asset)
        {
            if (asset.State == CustomTokenState.MoveCustomToken)
            {
                //Set threshold and weights of Issuer account as 0; so that no more coin can be minted.
                //All the coins should have been transferred to Distribution account by now.
                //Its the responsibility of the Distribution account to transfer the tokens to others.
                var weight = new HorizonAccountWeightModel
                {
                    MasterWeight = 0,
                    HighThreshold = 0,
                    MediumThreshold = 0,
                    LowThreshold = 0,
                    SignerSecret = null
                };

                //Let the SignerSecret be null
                var operations = new List<Operation>();
                var setOptionsOperation = SetOptionsOperation(asset.IssuerAccount.PublicKey, weight);
                operations.Add(setOptionsOperation);

                var xdrTransaction = await CreateTransaction(asset.IssuerAccount.PublicKey, operations, null, 0);
                await SubmitTransaction(SignTransaction(asset.IssuerAccount, null, xdrTransaction));
                asset.State = CustomTokenState.LockCustomToken;
                return 0;
            }

            return -1;
        }
    }
}