using Stellmart.Api.Data.Contract;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;
using stellar_dotnetcore_sdk;
using Transaction = stellar_dotnetcore_sdk.Transaction;
using XdrTransaction = stellar_dotnetcore_sdk.xdr.Transaction;


namespace Stellmart.Services.Contract
{
    public interface IContractService
    {
	/* Create and fund escrow account.
	   Add signer as destination, stellmart and change threshold weights.
	   Returns Escrow account id */
	HorizonKeyPairModel SetupContract(HorizonKeyPairModel SourceAccount, string DestAccount,
						string Amount);
	/* Create Pre Transactions and returns txn_1 and txn_2 XDR.
	   ContractModel to be stored in database.
	   TBD: Add data model parameter to support delivery date, waiting days etc to determine time bounds*/
	ContractModel CreateContract(HorizonKeyPairModel EscrowAccount, HorizonKeyPairModel DestAccount);

	string SignContract(HorizonKeyPairModel Account, ContractModel Contract);
	/*Submits the transaction to the network, returns the hash of transaction*/
	string ExecuteContract(XdrTransaction PreTxn);
    }
}
