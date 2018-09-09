namespace Stellmart.Api.Data.Horizon
{
    public class HorizonAssetModel
    {
	public bool IsNative { get; set; }
        public string Code { get; set; }
        public string Issuer { get; set; }
	public float Amount { get; set; }
    }
}
