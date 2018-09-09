using Stellmart.Api.Data.Contract;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;
using stellar_dotnetcore_sdk;


namespace Stellmart.Services.Interfaces
{
    public interface IContractService
    {
	/* Create and fund escrow account.
	   Add signer as destination, stellmart and change threshold weights.
	   Returns Escrow account id
         */
	Task<HorizonKeyPairModel> SetupContract(HorizonKeyPairModel SourceAccount, string DestAccount,
						string Amount);
	/* Based on the Contract Parameters, Contract will be created.
	   Returns Contract with one pre transaction added to the contract list
	 */
	ContractModel CreateContract(ContractParamModel ContractParam);

	string SignContract(HorizonKeyPairModel Account, ContractModel Contract);
	/*Submits the transaction to the network, returns the hash of transaction*/
	string ExecuteContract(ContractModel Contract);
    }
}
