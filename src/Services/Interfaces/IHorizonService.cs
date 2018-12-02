﻿using stellar_dotnet_sdk;
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
	    Task<long> GetSequenceNumber(string publicKey);
        Task<string> GetAccountBalance(string AccountPublicKey, string AssetType, string AssetIssuerPublicKey);
        Operation CreatePaymentOperation(string sourceAccountPublicKey, string destinationAccountPublicKey, HorizonAssetModel horizonAsset);
        Operation SetOptionsWeightOperation(string sourceAccountPublicKey, HorizonAccountWeightModel weights);
        Operation SetOptionsSingleSignerOperation(string secondSignerAccountPublicKey);
        Operation CreateAccountMergeOperation(string sourceAccountPublicKey, string destinationAccountPublicKey);
        Operation ChangeTrustOperation(string sourceAccountPublicKey, HorizonAssetModel assetModel, string limit);
        Operation BumpSequenceOperation(string sourceAccountPublicKey, long nextSequence);
        Operation CreateAccountOperation(string sourceAccountPublicKey, string destAccountPublicey,
                            string amount);
        Task<string> CreateTransaction(string sourceAccountPublicKey, List<Operation> operations, HorizonTimeBoundModel time, long sequence);
        string SignTransaction(HorizonKeyPairModel account, string secretKey, string xdrTransaction);
        Task<SubmitTransactionResponse> SubmitTransaction(string xdrTransaction);
        Task<bool> PaymentTransaction(HorizonKeyPairModel Source, string DestinationPublicKey,
                    HorizonAssetModel asset);
        string GetPublicKey(string secretKey);
        int GetSignatureCount(string xdrTransaction);
        string SignatureHash(string xdrTransaction, int index);
        Task<HorizonAssetModel> CreateAsset(string name, string limit);
        Task<bool> MoveAsset(HorizonAssetModel asset);
        Task<bool> LockAsset(HorizonAssetModel asset);
    }
}
