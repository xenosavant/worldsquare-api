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
        Operation CreatePaymentOperation(string sourceAccountPublicKey,
            string destAccountPublicKey, string amount);
        Operation SetOptionsOperation(string SourceAccountPublicKey,
            HorizonAccountWeightModel Weights);
        Operation CreateAccountMergeOperation(string SourceAccountPublicKey,
                    string destAccountPublicKey);
        Operation ChangeTrustOperation(string SourceAccountPublicKey, HorizonAssetModel AssetModel,
                    string limit);
        Operation BumpSequenceOperation(string SourceAccountPublicKey, long nextSequence);
        Task<string> CreateTransaction(string SourceAccountPublicKey, List<Operation> ops,
                    HorizonTimeBoundModel Time, long sequence);
        string SignTransaction(HorizonKeyPairModel Account, string secretkey, string txnstr);
        Task<SubmitTransactionResponse> SubmitTransaction(string txnstr);
        string GetPublicKey(string SecretKey);
        int GetSignatureCount(string txnstr);
        string SignatureHash(string txnstr, int index);
        Task<HorizonAssetModel> CreateAsset(string name, string limit);
        Task<int> MoveAsset(HorizonAssetModel asset);
        Task<int> LockAsset(HorizonAssetModel asset);



    }
}
