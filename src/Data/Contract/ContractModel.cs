using System;
using System.Collections.Generic;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Data.Contract
{
    public class ContractModel
    {
	HorizonKeyPairModel EscrowAccount { get; set; }
	HorizonKeyPairModel DestAccount { get; set; }
	HorizonKeyPairModel WorldSquareAccount { get; set; }
	int state { get; set; }

	public ICollection<ContractPreTxnModel> Transactions { get; set; }
    }
}
