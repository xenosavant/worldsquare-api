
using System.Collections.Generic;

namespace Stellmart.Api.Context.Entities.ReadOnly
{
    public class DistanceUnit : LookupData
    {
        public virtual ICollection<PricePerDistance> PricePerDistances { get; set; }
    }
}
