using System;
using System.Collections.Generic;
using Stellmart.Api.Data.Horizon;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;

namespace Stellmart.Api.Data.Contract
{
	public enum ContractState
	{
		Initial,
		Preliminary,
		Activated,
		Completed,
		Contested
	}
    public class ContractModel
    {
	public HorizonKeyPairModel EscrowAccount { get; set; }
	public String DestAccount { get; set; }
	public HorizonKeyPairModel WorldSquareAccount { get; set; }

	public HorizonAssetModel Asset { get; set; }
	public long Sequence { get; set; }
	public ContractState State { get; set; }

	public ICollection<ContractPhaseModel> Phases { get; set; }

	/*Total list of all pre transactions and signatures irrespective of phases*/
	public ICollection<ContractPreTxnModel> PreTransactions { get; set; }
	public ICollection<String> Signatures { get; set; }
	public ICollection<SubmitTransactionResponse> Txn {get; set; }
    }
}
