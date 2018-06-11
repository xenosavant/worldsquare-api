namespace Stellmart.Api.Data.Horizon
{
    public class HorizonAccountSignerModel
    {
        public HorizonKeyPairModel Signer { get; set; }
        public int Weight { get; set; }
    }
}
