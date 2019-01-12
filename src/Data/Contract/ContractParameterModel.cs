using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Data.Contract
{
    public class ContractParameterModel
    {
        public Obligation Obligation { get; set; }
        public HorizonAssetModel Asset { get; set; }
        public ContractSignatureModel SourceAccountSecret { get; set; }
        public string SourceAccountId { get; set; }
        public string DestinationAccountId { get; set; }
    }
}
