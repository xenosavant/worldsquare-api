using Stellmart.Api.Context.Entities.Entity;

namespace Stellmart.Api.Context.Entities
{
    public class GeoLocation : AuditableEntity<int>
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public virtual Location Location { get; set; }

        public virtual Area Area { get; set; }
    }
}
