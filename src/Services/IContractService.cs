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
	   Returns Escrow account id
         */
	HorizonKeyPairModel SetupContract(HorizonKeyPairModel SourceAccount, string DestAccount,
						string Amount);
	/* Based on the Contract Parameters, Contract will be created.
	   Contract escrow and destination accounts are necessary inputs.
	   Returns Contract with one pre transaction added to the contract list
	 */
	ContractModel CreateContract(ContractModel Contract,
					ContractParamModel ContractParam);

	string SignContract(HorizonKeyPairModel Account, ContractModel Contract);
	/*Submits the transaction to the network, returns the hash of transaction*/
	string ExecuteContract(ContractModel Contract);
    }
}
