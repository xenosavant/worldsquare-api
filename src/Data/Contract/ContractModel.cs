using stellar_dotnet_sdk.responses;
using Stellmart.Api.Data.Horizon;
using System;
using System.Collections.Generic;

namespace Stellmart.Api.Data.Contract
{
    public class ContractModel
    {
	// Current State of the contract
	public ContractState State { get; set; }
	// Current Phase of the contract
	public ContractPhase Phase {get; set;}
	//Current Sequence of contract / escrow account
	public long Sequence { get; set; }
	// Escrow account details
	public HorizonKeyPairModel EscrowAccount { get; set; }
	// Seller Account
	public String DestAccount { get; set; }
	//System Account
	public HorizonKeyPairModel WorldSquareAccount { get; set; }
	//asset details
	public HorizonAssetModel Asset { get; set; }
	public ICollection<ContractPhaseModel> Phases { get; set; }

	/*Total list of all pre transactions, signatures, txn hash*/
	public ICollection<ContractPreTxnModel> PreTransactions { get; set; }
	public ICollection<String> Signatures { get; set; }
	public ICollection<SubmitTransactionResponse> Txn {get; set; }
    }
}
