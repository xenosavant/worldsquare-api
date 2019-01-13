using System.Threading.Tasks;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contracts;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IContractService
    {
        Task<Contract> SetupContractAsync(ContractParameterModel contractParameterModel);
        Task<Contract> FundContractAsync(Contract contract, ContractSignatureModel fundingAccount, string amount);
        Task<Contract> CreateContractAsync(Contract contract);
        Task<bool> UpdateContractAsync(Contract contract);

        bool SignContract(ContractSignatureModel signature);
    }
}