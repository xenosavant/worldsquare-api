using AutoMapper;
using Microsoft.Extensions.Options;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            {
                Network.UseTestNetwork();
            }
            else
            {
                Network.UsePublicNetwork();
            }
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
            return _mapper.Map<HorizonFundTestAccountModel>(await _server.Accounts.Account(KeyPair.FromAccountId(publicKey)));
        }

        public async Task<long> GetSequenceNumber(string PublicKey)
        {
            var accountRes = await _server.Accounts.Account(KeyPair.FromAccountId(PublicKey));
            return accountRes.SequenceNumber;
        }

        public Operation CreatePaymentOps(HorizonKeyPairModel sourceAccount,
                    String destAccount, String amount) 
        {
            var source = KeyPair.FromSecretSeed(sourceAccount.SecretKey);
            Asset native = new AssetTypeNative();

            var operation = new PaymentOperation.Builder(KeyPair.FromAccountId(destAccount), native, amount)
                 .SetSourceAccount(source)
                 .Build();
            return operation;
        }
         public Operation SetOptionsOp(HorizonKeyPairModel SourceAccount,
            HorizonAccountWeightModel Weights)
        {
            var source = KeyPair.FromSecretSeed(SourceAccount.SecretKey);
            var operation = new SetOptionsOperation.Builder();

            operation.SetMasterKeyWeight(Weights.MasterWeight);
            operation.SetLowThreshold(Weights.LowThreshold);
            operation.SetMediumThreshold(Weights.MediumThreshold);
            operation.SetHighThreshold(Weights.HighThreshold);

            /*BUG: Second signer is not getting added */
            foreach (HorizonAccountSignerModel SignerAccount in Weights.Signers)
            {
                operation.SetSigner(stellar_dotnet_sdk.Signer.Ed25519PublicKey(KeyPair.FromAccountId(SignerAccount.Signer)), SignerAccount.Weight);
            }
            if(Weights.SignerSecret != null) {
                var hash = Util.Hash(Encoding.UTF8.GetBytes(Weights.SignerSecret.Secret));
                operation.SetSigner(stellar_dotnet_sdk.Signer.Sha256Hash(hash), Weights.SignerSecret.Weight);
            }
            operation.SetSourceAccount(source);
            return operation.Build();
        }
        public Operation CreateAccountMergeOps(HorizonKeyPairModel sourceAccount,
                    String destAccount) 
        {
            var source = KeyPair.FromSecretSeed(sourceAccount.SecretKey);

            var operation = new AccountMergeOperation.Builder(KeyPair.FromAccountId(destAccount))
                 .SetSourceAccount(source)
                 .Build();
            return operation;
        }
        public Operation ChangeTrustOps(HorizonKeyPairModel sourceAccount, HorizonAssetModel AssetModel,
                    String limit)
        {
            var source = KeyPair.FromSecretSeed(sourceAccount.SecretKey);
            Asset asset = new AssetTypeCreditAlphaNum4(AssetModel.Code, AssetModel.Issuer);

            var operation = new ChangeTrustOperation.Builder(asset, limit)
                .SetSourceAccount(source)
                .Build();
            return operation;
        }
        public Operation BumpSequenceOps(HorizonKeyPairModel sourceAccount,
                    long nextSequence)
        {
            var source = KeyPair.FromSecretSeed(sourceAccount.SecretKey);

            var operation = new BumpSequenceOperation.Builder(nextSequence)
                 .SetSourceAccount(source)
                 .Build();
            return operation;
        }
        private Transaction XdrStrtoTxn(string txnstr) 
        {
            var bytes = Convert.FromBase64String(txnstr);
            var transactionEnvelope = stellar_dotnet_sdk.xdr.TransactionEnvelope.Decode(new stellar_dotnet_sdk.xdr.XdrDataInputStream(bytes));
            return Transaction.FromEnvelopeXdr(transactionEnvelope);
        }
        
        public async Task<string> CreateTxn(HorizonKeyPairModel SourceAccount,
                                                List<Operation> ops, HorizonTimeBoundModel Time)
        {
            var source = KeyPair.FromSecretSeed(SourceAccount.SecretKey);
            var accountRes = await _server.Accounts.Account(KeyPair.FromAccountId(SourceAccount.PublicKey));
            var txn_builder = new Transaction.Builder(new Account(source, accountRes.SequenceNumber));
            foreach(Operation op in ops) {
                txn_builder.AddOperation(op);
            }
            if(Time != null)
                txn_builder.AddTimeBounds(new TimeBounds(Time.MinTime, Time.MaxTime));
            var transaction = txn_builder.Build();
            transaction.Sign(source);

            return transaction.ToEnvelopeXdrBase64();
        }
        public string SignTxn(HorizonKeyPairModel Account, string txnstr)
        {
            var txn = XdrStrtoTxn(txnstr);
            txn.Sign(KeyPair.FromSecretSeed(Account.SecretKey));
            return txn.ToEnvelopeXdrBase64();
        }
        public async Task<SubmitTransactionResponse> SubmitTxn(string txnstr)
        {
            return await _server.SubmitTransaction(XdrStrtoTxn(txnstr));
        }
        public async Task<HorizonAssetModel> CreateAsset(string name, string limit)
        {
            HorizonAssetModel asset = new HorizonAssetModel();
            asset.IsNative = false;
            asset.MaxCoinLimit = limit;
            asset.Code = name;
            HorizonKeyPairModel Issuer = CreateAccount();
            HorizonKeyPairModel Distributor = CreateAccount();
            //TBD : Real network code is pending
            //Fund minimum XLM to create operations
            await FundTestAccountAsync(Issuer.PublicKey);
            await FundTestAccountAsync(Distributor.PublicKey);
            asset.IssuerAccount = Issuer;
            asset.Distributor = Distributor;
            asset.Issuer = KeyPair.FromAccountId(Issuer.PublicKey);
            //Create trustline from Distributor to Issuer
            var Ops = new List<Operation>();
            var TrustOp = ChangeTrustOps(Distributor, asset, limit);
            Ops.Add(TrustOp);
            var txnxdr = await CreateTxn(Distributor, Ops, null);
            await SubmitTxn(SignTxn(Distributor, txnxdr));
            asset.State = CustomTokenState.CreateCustomToken;
            return asset;
        }
        public async Task<int> MoveAsset(HorizonAssetModel asset)
        {
            if(asset.State == CustomTokenState.CreateCustomToken)
            {
                var Ops = new List<Operation>();
                //TBD: PaymentOps supports only native currency XLM.
                //Add support for custom token transfer.
                var PaymentOp = CreatePaymentOps(asset.IssuerAccount, asset.Distributor.PublicKey,
                    asset.MaxCoinLimit);
                Ops.Add(PaymentOp);
                var txnxdr = await CreateTxn(asset.IssuerAccount, Ops, null);
                await SubmitTxn(SignTxn(asset.IssuerAccount, txnxdr));
                asset.State = CustomTokenState.MoveCustomToken;
                return 0;
            } else
                return -1;
        }
        public async Task<int> LockAsset(HorizonAssetModel asset)
        {
            if(asset.State == CustomTokenState.MoveCustomToken)
            {
                //Set threshold and weights of Issuer account as 0; so that no more coin can be minted.
                //All the coins should have been transferred to Distribution account by now.
                //Its the responsiblity of the Distribution account to transfer the tokens to others.
                HorizonAccountWeightModel weight = new HorizonAccountWeightModel();
                weight.MasterWeight = weight.HighThreshold = weight.MediumThreshold =
                    weight.LowThreshold = 0;
                //Let the SignerSecret be null
                weight.SignerSecret = null;
                var Ops = new List<Operation>();
                var SetOptOp = SetOptionsOp(asset.IssuerAccount, weight);
                Ops.Add(SetOptOp);
                var txnxdr = await CreateTxn(asset.IssuerAccount, Ops, null);
                await SubmitTxn(SignTxn(asset.IssuerAccount, txnxdr));
                asset.State = CustomTokenState.LockCustomToken;
                return 0;
            } else
                return -1;
        }
    }
}