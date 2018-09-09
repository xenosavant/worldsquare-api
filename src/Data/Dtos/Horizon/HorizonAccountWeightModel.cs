using System;
using System.Collections.Generic;
namespace Stellmart.Api.Data.Horizon
{
    public class HorizonAccountWeightModel
    {
        public int MasterWeight { get; set; }
        public int LowThreshold { get; set; }
        public int MediumThreshold { get; set; }
        public int HighThreshold { get; set; }
	 public List<HorizonAccountSignerModel> Signers { get; set; }
    }
}
