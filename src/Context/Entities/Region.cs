using Stellmart.Api.Context.Entities.Entity;

namespace Stellmart.Api.Context.Entities
{
    public class Region : Entity<int>
    {
       public string LocationComponents { get; set; }

       public virtual OnlineStore OnlineStore { get; set; }
    }
}
