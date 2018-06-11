﻿using AutoMapper;
using Microsoft.Extensions.Options;
using stellar_dotnetcore_sdk;
using response = stellar_dotnetcore_sdk.responses;
using Stellmart.Api.Data.Horizon;
using Stellmart.Api.Data.Settings;
using System;
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
            await _server.HttpClient.GetAsync($"friendbot?addr={publicKey}");

            //See our newly created account.
            return _mapper.Map<HorizonFundTestAccountModel>(await _server.Accounts.Account(KeyPair.FromAccountId(publicKey)));
         }
	public async Task <response.SubmitTransactionResponse> TransferNativeFund(HorizonKeyPairModel sourceAccount,
				HorizonKeyPairModel destAccount, String amount)
	{
	    var source = KeyPair.FromSecretSeed(sourceAccount.SecretKey);
	    Asset native = new AssetTypeNative();

           var operation = new PaymentOperation.Builder(KeyPair.FromSecretSeed(destAccount.SecretKey), native, amount)
                .SetSourceAccount(source)
                .Build();
	    var accountRes = await _server.Accounts.Account(KeyPair.FromAccountId(sourceAccount.PublicKey));
	    var transaction = new Transaction.Builder(new Account(source, accountRes.SequenceNumber))
                .AddOperation(operation)
                .Build();
           transaction.Sign(source);

	    return await _server.SubmitTransaction(transaction);
	}

	public async Task <response.SubmitTransactionResponse> SetWeightSigner(HorizonKeyPairModel SourceAccount,
		HorizonAccountWeightModel Weights) {
	   var source = KeyPair.FromSecretSeed(SourceAccount.SecretKey);
	   var operation = new SetOptionsOperation.Builder();
	   operation.SetMasterKeyWeight(Weights.MasterWeight);
	   operation.SetLowThreshold(Weights.LowThreshold);
	   operation.SetMediumThreshold(Weights.MediumThreshold);
	   operation.SetHighThreshold(Weights.HighThreshold);

	   /*BUG: Second signer is not getting added */
	   foreach(HorizonAccountSignerModel SignerAccount in Weights.Signers) {
			operation.SetSigner(Signer.Ed25519PublicKey(KeyPair.FromSecretSeed
				(SignerAccount.Signer.SecretKey)), SignerAccount.Weight);
		}
	   operation.SetSourceAccount(source);
	   var opBuild = operation.Build();

	   var accountRes = await _server.Accounts.Account(KeyPair.FromAccountId(SourceAccount.PublicKey));
	   var transaction = new Transaction.Builder(new Account(source, accountRes.SequenceNumber))
		.AddOperation(opBuild)
		.Build();
	   transaction.Sign(source);

	   return await _server.SubmitTransaction(transaction);
	}
    }
}
