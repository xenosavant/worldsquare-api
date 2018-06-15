using System;
using System.Collections.Generic;

namespace Stellmart.Api.Data.Contract
{
    public class ContractPreTxnModel
    {
	public long Sequence { get; set; }

	/*ToDo: Revisit Param model here*/
	public ContractParamModel Param { get; set; }

	public String XdrString { get; set; }
	public ICollection<String> Signatures { get; set; }
    }
}
