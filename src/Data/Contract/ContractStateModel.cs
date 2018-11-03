using System;
using System.Collections.Generic;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Data.Contract
{
    public class ContractStateModel
    {
    public ContractState StateName { get; set; }
    public bool status {get; set;} /* true for done  */
	public long ContractSequence { get; set; }
	public ICollection<ContractPreTxnModel> PreTransactions { get; set; }
    }
}
