using System.Threading.Tasks;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IHorizonServerManager
    {
        Task FundTestAccountAsync(string publicKey);
        Task<AccountResponse> GetAccountAsync(string publicKey);
        Task<SubmitTransactionResponse> SubmitTransaction(Transaction xdrTransaction);
    }
}
