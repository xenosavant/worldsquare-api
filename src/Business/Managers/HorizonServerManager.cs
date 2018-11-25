using System.Threading.Tasks;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Business.Managers.Interfaces;

namespace Stellmart.Api.Business.Managers
{
    public class HorizonServerManager : IHorizonServerManager
    {
        private readonly Server _server;

        public HorizonServerManager(Server server)
        {
            _server = server;
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
