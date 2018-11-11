using System;
using System.Collections.Generic;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Data.Contract
{
	public class ContractParamModel
    {
	public int ContractTypeId;
	public String SourceAccount {get; set; }
	public String DestAccount { get; set; }

	public HorizonAssetModel Asset { get; set; }

	/* Delay Param */
	public long MinTime { get; set; }
	public long MaxTime { get; set; }
    }
}
