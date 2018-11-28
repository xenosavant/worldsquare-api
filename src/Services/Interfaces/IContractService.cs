using System.Threading.Tasks;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Contract;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IContractService
    {
        Task<Contract> SetupContractAsync();
        Task<Contract> FundContractAsync(Contract contract, ContractParameterModel contractParameterModel);
        Task<Contract> CreateContractAsync(Contract contract);
        Task<bool> UpdateContractAsync(Contract contract);

        bool SignContract(ContractSignatureModel signature);
    }
}