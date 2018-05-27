using System.Collections.Generic;

namespace Stellmart.Api.ViewModels.Horizon
{
    public class HorizonFundTestAccountViewModel
    {
        public string PublicKey { get; set; }
        public IReadOnlyCollection<HorizonBalanceViewModel> Balances { get; set; }
    }
}
