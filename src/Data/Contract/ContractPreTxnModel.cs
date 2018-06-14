using System;
using System.Collections.Generic;
using Transaction = stellar_dotnetcore_sdk.Transaction;
using XdrTransaction = stellar_dotnetcore_sdk.xdr.Transaction;
namespace Stellmart.Api.Data.Contract
{
    public class ContractPreTxnModel
    {
	public long Sequence { get; set; }
	public ContractParamModel Param { get; set; }
       public XdrTransaction Xdr { get; set; }
    }
}
