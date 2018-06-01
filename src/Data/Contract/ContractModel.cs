using Transaction = stellar_dotnetcore_sdk.Transaction;
using XdrTransaction = stellar_dotnetcore_sdk.xdr.Transaction;
namespace Stellmart.Api.Data.Contract
{
    public class ContractModel
    {
        public XdrTransaction PreTxn1 { get; set; }
        public XdrTransaction PreTxn2 { get; set; }
	/* TBD: Add more fields to define contract */
    }
}
