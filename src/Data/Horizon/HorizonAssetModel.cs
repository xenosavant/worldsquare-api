using stellar_dotnet_sdk;
namespace Stellmart.Api.Data.Horizon
{
    public class HorizonAssetModel
    {
	public bool IsNative { get; set; }
        public string Code { get; set; }
        public KeyPair Issuer { get; set; }
	public string Amount { get; set; }
    }
}
