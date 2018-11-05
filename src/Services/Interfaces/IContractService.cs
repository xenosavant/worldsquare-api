﻿using Stellmart.Api.Data.Contract;
using Stellmart.Api.Data.Horizon;
using System.Threading.Tasks;
using Stellmart.Api.Context.Entities;


namespace Stellmart.Api.Services.Interfaces
{
    public interface IContractService
    {
	/* Create and fund escrow account.
	   Add signer as destination, stellmart and change threshold weights.
	   Returns Escrow account id
         */
	Task<Contract> SetupContract();
	 Task<Contract> FundContract(ContractParamModel ContractParam, Contract ContractFund);
	Task<Contract> CreateContract(ContractParamModel ContractParam, Contract contract);

	string SignContract(ContractSignatureModel signature);
	/*Submits the transaction to the network, returns the hash of transaction*/
	string ExecuteContract();
    }
}
