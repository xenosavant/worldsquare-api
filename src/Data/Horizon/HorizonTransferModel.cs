using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Horizon
{
    public class HorizonTransferModel
    {
        public HorizonKeyPairModel SourceAccount { get; set; }
        public HorizonKeyPairModel DestinationAccount { get; set; }
        public HorizonAssetModel Asset { get; set; }
    }
}
