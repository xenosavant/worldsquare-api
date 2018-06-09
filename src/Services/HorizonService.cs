using AutoMapper;
using Microsoft.Extensions.Options;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
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
	public async Task <SubmitTransactionResponse> TransferNativeFund(HorizonKeyPairModel sourceAccount,
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
    }
}
