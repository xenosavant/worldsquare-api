using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Data.Horizon;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IHorizonService
    {
        HorizonKeyPairModel CreateAccount();
        Task<HorizonFundTestAccountModel> FundTestAccountAsync(string publicKey);
	    Task<long> GetSequenceNumber(string PublicKey);
        Operation CreatePaymentOps(string sourceAccountPublicKey,
            string destAccountPublicKey, string amount);
        Operation SetOptionsOp(string SourceAccountPublicKey,
            HorizonAccountWeightModel Weights);
        Operation CreateAccountMergeOps(string SourceAccountPublicKey,
                    string destAccountPublicKey);
        Operation ChangeTrustOps(string SourceAccountPublicKey, HorizonAssetModel AssetModel,
                    string limit);
        Operation BumpSequenceOps(string SourceAccountPublicKey, long nextSequence);
        Task<string> CreateTxn(string SourceAccountPublicKey, List<Operation> ops,
                    HorizonTimeBoundModel Time, long sequence);
        string SignTxn(HorizonKeyPairModel Account, string txnstr);
        Task<SubmitTransactionResponse> SubmitTxn(string txnstr);
        string GetPublicKey(string SecretKey);
        Task<HorizonAssetModel> CreateAsset(string name, string limit);
        Task<int> MoveAsset(HorizonAssetModel asset);
        Task<int> LockAsset(HorizonAssetModel asset);



    }
}
