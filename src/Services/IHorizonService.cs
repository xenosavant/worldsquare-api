using stellar_dotnet_sdk.responses;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;

namespace Stellmart.Services
{
    public interface IHorizonService
    {
        HorizonKeyPairModel CreateAccount();
        Task<HorizonFundTestAccountModel> FundTestAccountAsync(string publicKey);
	    Task<long> GetSequenceNumber(string PublicKey);
        Task<string> TransferNativeFund(HorizonKeyPairModel sourceAccount,
			string destAccount, string amount);
	    Task<string> SetWeightSigner(HorizonKeyPairModel SourceAccount,
			HorizonAccountWeightModel Weights);
        Task<SubmitTransactionResponse> SubmitTxn(string txnstr);
    }
}
