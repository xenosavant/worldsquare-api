using System;
using System.Collections.Generic;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Data.Contract
{
    public class ContractParamModel
    {
	public HorizonKeyPairModel EscrowAccount { get; set; }
	public HorizonKeyPairModel DestAccount { get; set; }

	public HorizonAssetModel Asset { get; set; }

	public int MasterWeight { get; set; }
	public int LowThreshold { get; set; }
	public int MediumThreshold { get; set; }
	public int HighThreshold { get; set; }

	/* Delay Param */
	public long MinTime { get; set; }
	public long MaxTime { get; set; }
	public ICollection<ContractPreCondition> PreCondition { get; set; }
    }
}
