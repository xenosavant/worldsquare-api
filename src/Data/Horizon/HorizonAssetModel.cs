using stellar_dotnet_sdk;
namespace Stellmart.Api.Data.Horizon
{
    public enum CustomTokenState
    {
        NotaCustomToken = 0,
        CreateCustomToken,
        MoveCustomToken,
        LockCustomToken
    }

    public class HorizonAssetModel
    {
	    public bool IsNative { get; set; }
        public string Code { get; set; }
        public KeyPair Issuer { get; set; }
        public string Amount {get; set;}
        //these fields are used during custom token creation

        public CustomTokenState State {get; set;}
        public HorizonKeyPairModel IssuerAccount {get; set;}
        public HorizonKeyPairModel Distributor {get; set; }
        public string MaxCoinLimit {get; set; }
    }
}
