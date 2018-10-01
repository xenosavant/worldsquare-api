using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IHorizonService
    {
        HorizonKeyPairModel CreateAccount();
        Task<HorizonFundTestAccountModel> FundTestAccountAsync(string publicKey);
	    Task<long> GetSequenceNumber(string PublicKey);
        Operation CreatePaymentOps(HorizonKeyPairModel sourceAccount,
            string destAccount, string amount);
        Operation SetOptionsOp(HorizonKeyPairModel SourceAccount,
            HorizonAccountWeightModel Weights);
        Operation CreateAccountMergeOps(HorizonKeyPairModel sourceAccount,
                    string destAccount); 
        Operation ChangeTrustOps(HorizonKeyPairModel sourceAccount, HorizonAssetModel AssetModel,
                    string limit);
        Task<string> CreateTxn(HorizonKeyPairModel SourceAccount, List<Operation> ops,
                    HorizonTimeBoundModel Time);
        string SignTxn(HorizonKeyPairModel Account, string txnstr);
        Task<SubmitTransactionResponse> SubmitTxn(string txnstr);
        Task<HorizonAssetModel> CreateAsset(string name, string limit);
        Task<int> MoveAsset(HorizonAssetModel asset);
        Task<int> LockAsset(HorizonAssetModel asset);



    }
}
