using System;
using System.Collections.Generic;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Data.Contract
{
    public class ContractPhaseModel
    {
	public long ContractSequence { get; set; }
	public int ContractPhaseState { get; set; }
        public int TimeDelay { get; set; }

	public ICollection<ContractPreTxnModel> PreTransactions { get; set; }
    }
}
