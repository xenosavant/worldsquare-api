using AutoMapper;
using Microsoft.Extensions.Options;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            await _server.HttpClient.GetAsync($"friendbot?addr={publicKey}");

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
        public async Task<string> SetWeightSigner(HorizonKeyPairModel SourceAccount,
            HorizonAccountWeightModel Weights, HorizonTimeBoundModel Time)
        {
            var source = KeyPair.FromSecretSeed(SourceAccount.SecretKey);
            var operation = new SetOptionsOperation.Builder();
            Transaction transaction;

            operation.SetMasterKeyWeight(Weights.MasterWeight);
            operation.SetLowThreshold(Weights.LowThreshold);
            operation.SetMediumThreshold(Weights.MediumThreshold);
            operation.SetHighThreshold(Weights.HighThreshold);

            /*BUG: Second signer is not getting added */
            foreach (HorizonAccountSignerModel SignerAccount in Weights.Signers)
            {
                operation.SetSigner(stellar_dotnet_sdk.Signer.Ed25519PublicKey(KeyPair.FromAccountId(SignerAccount.Signer)), SignerAccount.Weight);
            }
            operation.SetSourceAccount(source);
            var opBuild = operation.Build();

            var accountRes = await _server.Accounts.Account(KeyPair.FromAccountId(SourceAccount.PublicKey));
            if(Time == null) {
                transaction = new Transaction.Builder(new Account(source, accountRes.SequenceNumber))
                    .AddOperation(opBuild)
                    .Build();
            } else {
                transaction = new Transaction.Builder(new Account(source, accountRes.SequenceNumber))
                    .AddOperation(opBuild)
                    .AddTimeBounds(new TimeBounds(Time.MinTime, Time.MaxTime))
                    .Build();
            }
            transaction.Sign(source);
            return transaction.ToEnvelopeXdrBase64();
        }
        public async Task<string> AccountMerge(HorizonKeyPairModel SourceAccount,
            string DestAccount, HorizonTimeBoundModel Time)
        {
            var source = KeyPair.FromSecretSeed(SourceAccount.SecretKey);
            Transaction transaction;

            var operation = new AccountMergeOperation.Builder(KeyPair.FromAccountId(DestAccount))
                 .SetSourceAccount(source)
                 .Build();

            var accountRes = await _server.Accounts.Account(KeyPair.FromAccountId(SourceAccount.PublicKey));
            if(Time == null) {
                transaction = new Transaction.Builder(new Account(source, accountRes.SequenceNumber))
                    .AddOperation(operation)
                    .Build();
            } else {
                transaction = new Transaction.Builder(new Account(source, accountRes.SequenceNumber))
                    .AddOperation(operation)
                    .AddTimeBounds(new TimeBounds(Time.MinTime, Time.MaxTime))
                    .Build();
            }
            transaction.Sign(source);
            return transaction.ToEnvelopeXdrBase64();
        }

        private Transaction XdrStrtoTxn(string txnstr) 
        {
            var bytes = Convert.FromBase64String(txnstr);
            var transactionEnvelope = stellar_dotnet_sdk.xdr.TransactionEnvelope.Decode(new stellar_dotnet_sdk.xdr.XdrDataInputStream(bytes));
            return Transaction.FromEnvelope(transactionEnvelope);
        }
        
        public async Task<string> CreateTxn(HorizonKeyPairModel SourceAccount,
                                                List<Operation> ops)
        {
            var source = KeyPair.FromSecretSeed(SourceAccount.SecretKey);
            var accountRes = await _server.Accounts.Account(KeyPair.FromAccountId(SourceAccount.PublicKey));
            var txn_builder = new Transaction.Builder(new Account(source, accountRes.SequenceNumber));
            foreach(Operation op in ops) {
                txn_builder.AddOperation(op);
            }
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
    }
}