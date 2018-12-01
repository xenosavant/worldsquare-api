using System;
using System.Collections.Generic;
namespace Stellmart.Api.Data.Horizon
{
    public class HorizonAccountWeightModel
    {
        public int MasterWeight { get; set; } = -1;
        public int LowThreshold { get; set; } = -1;
        public int MediumThreshold { get; set; } = -1;
        public int HighThreshold { get; set; } = -1;
	 public List<HorizonAccountSignerModel> Signers { get; set; }
        public HorizonHashSignerModel SignerSecret { get; set;} = null;
    }
}
