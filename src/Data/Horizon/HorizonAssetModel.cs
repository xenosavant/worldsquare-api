namespace Stellmart.Api.Data.Horizon
{
    public class HorizonAssetModel
    {
        //for native, assetType is "native"
	    public string AssetType { get; set; }
        public string Amount {get; set;}
        //The below 2 fields are not required for native asset / XLM
        public string AssetCode { get; set; }
        public string AssetIssuerPublicKey { get; set; }
        public string SourceAccountPublicKey { get; set; }
        public string DestinationAccountPublicKey { get; set; }
    }
}
