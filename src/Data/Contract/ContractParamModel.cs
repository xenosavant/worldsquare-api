using System;
using System.Collections.Generic;

namespace Stellmart.Api.Data.Contract
{
    public class ContractParamModel
    {
        public int MasterWeight { get; set; }
        public int LowThreshold { get; set; }
        public int MediumThreshold { get; set; }
        public int HighThreshold { get; set; }
        public long MinTime { get; set; }
        public long MaxTime { get; set; }
    }
}
