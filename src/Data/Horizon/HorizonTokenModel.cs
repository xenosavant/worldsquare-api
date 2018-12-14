using Stellmart.Api.Data.Enums;

namespace Stellmart.Api.Data.Horizon
{
    public class HorizonTokenModel
    {
        public HorizonAssetModel HorizonAssetModel { get; set; }

        public CustomTokenState State { get; set; }

        public HorizonKeyPairModel IssuerAccount { get; set; }

        public HorizonKeyPairModel Distributor { get; set; }

        public string MaxCoinLimit { get; set; }
    }
}