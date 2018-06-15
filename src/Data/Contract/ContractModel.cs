using System;
using System.Collections.Generic;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Data.Contract
{
    public class ContractModel
    {
	public HorizonKeyPairModel EscrowAccount { get; set; }
	public HorizonKeyPairModel DestAccount { get; set; }
	public HorizonKeyPairModel WorldSquareAccount { get; set; }

	public HorizonAssetModel Asset { get; set; }
	public long ContractSequence { get; set; }
	public int ContractState { get; set; }

	public ICollection<ContractPhaseModel> Phases { get; set; }

	/*Total list of all pre transactions and signatures irrespective of phases*/
	public ICollection<ContractPreTxnModel> PreTransactions { get; set; }
	public ICollection<String> Signatures { get; set; }
    }
}
