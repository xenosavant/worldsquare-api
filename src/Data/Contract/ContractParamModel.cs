using System;
using System.Collections.Generic;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Data.Contract
{
	public enum ContractType
	{
		Setup,
		PreTxnAccountMerge,
		PreTxnSetWeight
	}
    public class ContractParamModel
    {
	public ContractType Type { get; set; }
	public HorizonKeyPairModel SourceAccount {get; set; }
	public HorizonKeyPairModel EscrowAccount { get; set; }
	public String DestAccount { get; set; }

	public HorizonAssetModel Asset { get; set; }

	/* Delay Param */
	public long MinTime { get; set; }
	public long MaxTime { get; set; }
	public ICollection<ContractPreCondition> PreCondition { get; set; }
    }
}
