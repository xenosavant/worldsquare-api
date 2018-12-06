using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Data.Contract
{
    public class ContractParameterModel
    {
	public int ContractTypeId;
	public string SourceAccount {get; set; }
	public string DestinationAccount { get; set; }
	public ContractSignatureModel SourceAccountSecret {get; set;}

	public HorizonAssetModel Asset { get; set; }

	/* Delay Parameter */
	public long MinTime { get; set; }
	public long MaxTime { get; set; }
    }
}
