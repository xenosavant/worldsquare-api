using System;
using System.Collections.Generic;

namespace Stellmart.Api.Data.Contract
{
    public class ContractPreTransactionModel
    {
	public long Sequence { get; set; }

	/*ToDo: Revisit Param model here*/
	public ContractParameterModel ParameterModel { get; set; }

	public string xdrString { get; set; }
	public IReadOnlyCollection<string> Signatures { get; set; }
    }
}
