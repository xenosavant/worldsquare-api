using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using Stellmart.Api.Data.Horizon;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IHorizonService
    {
        /// <summary>
        /// Creates stellar account
        /// </summary>
        /// <returns>Account key pair</returns>
        HorizonKeyPairModel CreateAccount();

        /// <summary>
        /// Funds stellar account via friend bot
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns>Account response with balance</returns>
        Task<HorizonFundTestAccountModel> FundTestAccountAsync(string publicKey);

        /// <summary>
        /// Gets sequence number of current account
        /// </summary>
        /// <param name="publicKey"></param>
        /// <returns>Sequence number</returns>
        Task<long> GetSequenceNumberAsync(string publicKey);


        /// <summary>
        /// Gets account balance
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Account balance</returns>
        Task<string> GetAccountBalanceAsync(HorizonAssetModel model);

        /// <summary>
        /// Operation for creating payment
        /// </summary>
        /// <param name="sourceAccountPublicKey"></param>
        /// <param name="destinationAccountPublicKey"></param>
        /// <param name="horizonAsset"></param>
        /// <returns></returns>
        Task<PaymentOperation> CreatePaymentOperationAsync(HorizonAssetModel model);

        /// <summary>
        /// Operation to set weights
        /// </summary>
        /// <param name="sourceAccountPublicKey"></param>
        /// <param name="weights"></param>
        /// <returns></returns>
        Operation SetOptionsWeightOperation(string sourceAccountPublicKey, HorizonAccountWeightModel weights);

        /// <summary>
        /// Operation to set single signer options
        /// </summary>
        /// <param name="secondSignerAccountPublicKey"></param>
        /// <returns></returns>
        Operation SetOptionsSingleSignerOperation(string secondSignerAccountPublicKey);

        /// <summary>
        /// Operation for merging account
        /// </summary>
        /// <param name="sourceAccountPublicKey"></param>
        /// <param name="destinationAccountPublicKey"></param>
        /// <returns></returns>
        Operation CreateAccountMergeOperation(string sourceAccountPublicKey, string destinationAccountPublicKey);

        /// <summary>
        /// Operation to change trust
        /// </summary>
        /// <param name="sourceAccountPublicKey"></param>
        /// <param name="assetModel"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Operation ChangeTrustOperation(string sourceAccountPublicKey, HorizonAssetModel assetModel, string limit);

        /// <summary>
        /// Bump sequence operation
        /// </summary>
        /// <param name="sourceAccountPublicKey"></param>
        /// <param name="nextSequence"></param>
        /// <returns></returns>
        Operation BumpSequenceOperation(string sourceAccountPublicKey, long nextSequence);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceAccountPublicKey"></param>
        /// <param name="destAccountPublicKey"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Operation CreateAccountOperation(string sourceAccountPublicKey, string destAccountPublicKey, string amount);

        /// <summary>
        /// Create transaction
        /// </summary>
        /// <param name="sourceAccountPublicKey"></param>
        /// <param name="operations"></param>
        /// <param name="time"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Task<string> CreateTransaction(string sourceAccountPublicKey, List<Operation> operations, HorizonTimeBoundModel time, long sequence);

        /// <summary>
        /// Sign transaction
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="xdrTransaction"></param>
        /// <returns></returns>
        string SignTransaction(string secretKey, string xdrTransaction);

        /// <summary>
        /// Submit transaction
        /// </summary>
        /// <param name="xdrTransaction"></param>
        /// <returns></returns>
        Task<SubmitTransactionResponse> SubmitTransaction(string xdrTransaction);

        /// <summary>
        /// Payment transaction
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        Task<bool> PaymentTransaction(HorizonAssetModel asset, string secretKey);

        /// <summary>
        /// Get public key from private key
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        string GetPublicKey(string secretKey);

        /// <summary>
        /// Get signature count
        /// </summary>
        /// <param name="xdrTransaction"></param>
        /// <returns></returns>
        int GetSignatureCount(string xdrTransaction);

        /// <summary>
        /// Get signature hash
        /// </summary>
        /// <param name="xdrTransaction"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        string SignatureHash(string xdrTransaction, int index);

    }
}
