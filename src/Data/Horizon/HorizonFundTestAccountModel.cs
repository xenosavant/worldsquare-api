using System.Collections.Generic;

namespace Stellmart.Api.Data.Horizon
{
    public class HorizonFundTestAccountModel
    {
        public string PublicKey { get; set; }
        public HorizonBalanceModel[] Balances { get; set; }
    }
}
