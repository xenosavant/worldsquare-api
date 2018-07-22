using Stellmart.Api.Data.Contract;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;


namespace Stellmart.Services
{
    public interface IContractService
    {
	/* Create and fund escrow account.
	   Add signer as destination, stellmart and change threshold weights.
	   Returns Escrow account id
         */
	Task<ContractModel> SetupContract(ContractParamModel ContractParam);
	/* Based on the Contract Parameters, Contract will be created.
	   Returns Contract with one pre transaction added to the contract list
	 */
	Task<ContractModel> CreateContract(ContractParamModel ContractParam, ContractModel Contract);

	string SignContract(HorizonKeyPairModel Account, ContractModel Contract);
	/*Submits the transaction to the network, returns the hash of transaction*/
	string ExecuteContract(ContractModel Contract);
    }
}
