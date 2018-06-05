using stellar_dotnetcore_sdk;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;

namespace Stellmart.Services
{
    public interface IHorizonService
    {
        HorizonKeyPairModel CreateAccount();
        Task<HorizonFundTestAccountModel> FundTestAccountAsync(string publicKey);
    }
}
