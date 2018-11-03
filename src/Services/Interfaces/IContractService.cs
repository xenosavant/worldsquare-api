using Stellmart.Api.Data.Contract;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;


namespace Stellmart.Api.Services.Interfaces
{
    public interface IContractService
    {
	/* Create and fund escrow account.
	   Add signer as destination, stellmart and change threshold weights.
	   Returns Escrow account id
         */
	Task<int> SetupContract(ContractParamModel ContractParam);
	/* Based on the Contract Parameters, Contract will be created.
	   Returns Contract with one pre transaction added to the contract list
	 */
	Task<int> CreateContract(ContractParamModel ContractParam);

	string SignContract(ContractSignatureModel signature);
	/*Submits the transaction to the network, returns the hash of transaction*/
	string ExecuteContract();
    }
}
