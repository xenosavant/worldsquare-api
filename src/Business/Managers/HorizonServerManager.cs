using System.Threading.Tasks;
using AutoMapper;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Business.Managers
{
    public class HorizonServerManager : IHorizonServerManager
    {
        private readonly Server _server;
        private readonly IMapper _mapper;

        public HorizonServerManager(Server server, IMapper mapper)
        {
            _server = server;
            _mapper = mapper;
        }

        public HorizonKeyPairModel CreateAccount()
        {
            return _mapper.Map<HorizonKeyPairModel>(KeyPair.Random());
        }

        public async Task FundTestAccountAsync(string publicKey)
        {
            await Server.HttpClient.GetAsync($"friendbot?addr={publicKey}");
        }

        public async Task<AccountResponse> GetAccountAsync(string publicKey)
        {
            var keyPair = KeyPair.FromAccountId(publicKey);
            return await _server.Accounts.Account(keyPair);
        }

        public async Task<SubmitTransactionResponse> SubmitTransaction(Transaction xdrTransaction)
        {
            return await _server.SubmitTransaction(xdrTransaction);
        }
    }
}
