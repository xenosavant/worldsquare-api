using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Stellmart.Services
{
    public interface IHorizonService
    {
        HorizonKeyPairModel CreateAccount();
        Task<HorizonFundTestAccountModel> FundTestAccountAsync(string publicKey);
	    Task<long> GetSequenceNumber(string PublicKey);
        Operation CreatePaymentOps(HorizonKeyPairModel sourceAccount,
            string destAccount, string amount);
	    Task<string> SetWeightSigner(HorizonKeyPairModel SourceAccount,
			HorizonAccountWeightModel Weights, HorizonTimeBoundModel Time);
        Task<string> AccountMerge(HorizonKeyPairModel SourceAccount,
            string destAccount, HorizonTimeBoundModel Time);
        Task<string> CreateTxn(HorizonKeyPairModel SourceAccount, List<Operation> ops);
        string SignTxn(HorizonKeyPairModel Account, string txnstr);
        Task<SubmitTransactionResponse> SubmitTxn(string txnstr);
    }
}
