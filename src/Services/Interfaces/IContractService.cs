using System.Threading.Tasks;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contract;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IContractService
    {
        /* Create and fund escrow account.
           Add signer as destination, worldsquare and change threshold weights.
           Returns Escrow account id
             */
        Task<Contract> SetupContractAsync(ContractParameterModel contractParameterModel);
        Task<Contract> FundContractAsync(Contract contract, ContractParameterModel contractParameterModel);
        Task<Contract> CreateContractAsync(Contract contract);

        bool SignContract(ContractSignatureModel signature);

        /*Submits the transaction to the network, returns the hash of transaction*/
        string ExecuteContract();
    }
}