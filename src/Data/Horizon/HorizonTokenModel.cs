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

    public class HorizonTokenModel: HorizonAssetModel
    {
        //these fields are used during custom token creation
        public HorizonAssetModel Asset;
        public CustomTokenState State {get; set;}
        public HorizonKeyPairModel IssuerAccount {get; set;}
        public HorizonKeyPairModel Distributor {get; set; }
        public string MaxCoinLimit {get; set; }
    }
}
