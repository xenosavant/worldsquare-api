using stellar_dotnetcore_sdk;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;

namespace Stellmart.Services.Contract
{
    public interface IContractService
    {
	/* Escrow account to be created by SourceAccount, SourceAccount has access of Escrow SecretSeed */
	HorizonKeyPairModel CreateEscrowAccount(HorizonKeyPairModel SourceAccount);
	/* Fund the Escrow account and returns hash of the fund escrow account transaction */
	byte[] FundEscrowAccount(HorizonKeyPairModel SourceAccount,string EscrowAccountPublicKey, string Amount);
	/* Set individual weights and threshold, returns hash of the transaction */
	byte[] SetEscrowWeightThreshold(HorizonKeyPairModel SourceAccount, string DestAccountPublicKey, int DestAccountWeight,
				string StellmartPublicKey, int StellmartWeight, int Threshold);
	/* Create Pre transactions with the same sequence number, one for success and another for failure.
	   Source Account signs the transaction, returns the XDR string */
	string CreatePreTxn(HorizonKeyPairModel SourceAccount, string DestAccountPublicKey, string StellmartPublicKey, 
			byte[] EscrowHash, long MinTime, long MaxTime);
	/* Destination account and Stellmart signs the pre transaction, returns the signed transaction XDR */
	string SignPreTxn(HorizonKeyPairModel Account, string PreTxnXDR);
	/*Submit Success or Failure transaction XDR */
	byte[] SubmitPreTxn(HorizonKeyPairModel Account, string TxnXDR);
    }
}
