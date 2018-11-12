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

        public Operation CreatePaymentOps(String sourceAccountPublicKey,
                    String destAccountPublicKey, String amount)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);
            Asset native = new AssetTypeNative();

            var operation = new PaymentOperation.Builder(KeyPair.FromAccountId(destAccountPublicKey), native, amount)
                 .SetSourceAccount(source)
                 .Build();
            return operation;
        }
         public Operation SetOptionsOp(string SourceAccountPublicKey,
            HorizonAccountWeightModel Weights)
        {
            var source = KeyPair.FromAccountId(SourceAccountPublicKey);
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
        public Operation CreateAccountMergeOps(String sourceAccountPublicKey,
                    String destAccountPublicKey)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);

            var operation = new AccountMergeOperation.Builder(KeyPair.FromAccountId(destAccountPublicKey))
                 .SetSourceAccount(source)
                 .Build();
            return operation;
        }
        public Operation ChangeTrustOps(string sourceAccountPublicKey, HorizonAssetModel AssetModel,
                    String limit)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);
            Asset asset = new AssetTypeCreditAlphaNum4(AssetModel.Code, AssetModel.Issuer);

            var operation = new ChangeTrustOperation.Builder(asset, limit)
                .SetSourceAccount(source)
                .Build();
            return operation;
        }
        public Operation BumpSequenceOps(string sourceAccountPublicKey,
                    long nextSequence)
        {
            var source = KeyPair.FromAccountId(sourceAccountPublicKey);

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
        
        public async Task<string> CreateTxn(string SourceAccountPublicKey,
                            List<Operation> ops, HorizonTimeBoundModel Time, long seq)
        {
            var source = KeyPair.FromAccountId(SourceAccountPublicKey);
            var accountRes = await _server.Accounts.Account(source);
            Transaction.Builder txn_builder;
            if(seq == 0) {
                txn_builder = new Transaction.Builder(new Account(source, accountRes.SequenceNumber));
            } else {
                txn_builder = new Transaction.Builder(new Account(source, seq));
            }
            foreach(Operation op in ops) {
                txn_builder.AddOperation(op);
            }
            if(Time != null)
                txn_builder.AddTimeBounds(new TimeBounds(Time.MinTime, Time.MaxTime));
            var transaction = txn_builder.Build();

            return transaction.ToUnsignedEnvelopeXdrBase64();
        }
        public string SignTxn(HorizonKeyPairModel Account, string secretkey, string txnstr)
        {
            var txn = XdrStrtoTxn(txnstr);
            txn.Sign(KeyPair.FromSecretSeed((secretkey==null)?Account.SecretKey:secretkey));
            return txn.ToEnvelopeXdrBase64();
        }
        public async Task<SubmitTransactionResponse> SubmitTxn(string txnstr)
        {
            return await _server.SubmitTransaction(XdrStrtoTxn(txnstr));
        }
        public string GetPublicKey(string SecretKey)
        {
            var keypair = KeyPair.FromSecretSeed(SecretKey);
            return keypair.AccountId;
        }
        public int GetSignatureCount(string txnstr)
        {
            var txn = XdrStrtoTxn(txnstr);
            return txn.Signatures.Count;
        }
        public string SignatureHash(string txnstr, int index)
        {
            var txn = XdrStrtoTxn(txnstr);
            return Encoding.UTF8.GetString(txn.Signatures[index].Signature.InnerValue);
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
            var TrustOp = ChangeTrustOps(Distributor.PublicKey, asset, limit);
            Ops.Add(TrustOp);
            var txnxdr = await CreateTxn(Distributor.PublicKey, Ops, null, 0);
            await SubmitTxn(SignTxn(Distributor, null, txnxdr));
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
                var PaymentOp = CreatePaymentOps(asset.IssuerAccount.PublicKey, asset.Distributor.PublicKey,
                    asset.MaxCoinLimit);
                Ops.Add(PaymentOp);
                var txnxdr = await CreateTxn(asset.IssuerAccount.PublicKey, Ops, null, 0);
                await SubmitTxn(SignTxn(asset.IssuerAccount, null, txnxdr));
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
                var SetOptOp = SetOptionsOp(asset.IssuerAccount.PublicKey, weight);
                Ops.Add(SetOptOp);
                var txnxdr = await CreateTxn(asset.IssuerAccount.PublicKey, Ops, null, 0);
                await SubmitTxn(SignTxn(asset.IssuerAccount, null, txnxdr));
                asset.State = CustomTokenState.LockCustomToken;
                return 0;
            } else
                return -1;
        }
    }
}