using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IHorizonService
    {
        HorizonKeyPairModel CreateAccount();
        Task<HorizonFundTestAccountModel> FundTestAccountAsync(string publicKey);
	Task<long> GetSequenceNumber(string PublicKey);
        Task<SubmitTransactionResponse> TransferNativeFund(HorizonKeyPairModel sourceAccount,
			string destAccount, string amount);
	 Task <SubmitTransactionResponse> SetWeightSigner(HorizonKeyPairModel SourceAccount,
			HorizonAccountWeightModel Weights);
    }
}
