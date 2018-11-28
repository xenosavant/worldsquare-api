using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IHorizonServerManager
    {
        HorizonKeyPairModel CreateAccount();
        Task FundTestAccountAsync(string publicKey);
        Task<AccountResponse> GetAccountAsync(string publicKey);
        Task<SubmitTransactionResponse> SubmitTransaction(Transaction xdrTransaction);
    }
}
