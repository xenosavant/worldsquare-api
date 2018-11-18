using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contract;
using System.Threading.Tasks;


namespace Stellmart.Api.Services.Interfaces
{
    public interface IContractService
    {
	/* Create and fund escrow account.
	   Add signer as destination, worldsquare and change threshold weights.
	   Returns Escrow account id
         */
	Task<Contract> SetupContract(ContractParameterModel contractParameterModel);
	Task<Contract> FundContract(Contract contract, ContractParameterModel contractParameterModel);
	Task<Contract> CreateContract(Contract contract);

    Task<bool> SignContract(ContractSignatureModel signature);
	/*Submits the transaction to the network, returns the hash of transaction*/
	string ExecuteContract();
    }
}
