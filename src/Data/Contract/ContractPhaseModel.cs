using System;
using System.Collections.Generic;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Data.Contract
{
    public class ContractPhaseModel
    {
    public ContractPhase PhaseName { get; set; }
	public long ContractSequence { get; set; }
    public bool status {get; set;} /* true for done  */
    //Pretransactions which needs to be signed.
    public ICollection<ContractPreTxnModel> PreTransactions { get; set; }
    //Contains list of signatures required to proceed further along with time delay
	public ContractPreCondition Condition {get; set;}
    }
}
