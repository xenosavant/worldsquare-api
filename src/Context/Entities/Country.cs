using Stellmart.Api.Context.Entities.Entity;

namespace Stellmart.Api.Context.Entities
{
    public class Country : Entity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string PhonePrefix { get; set; }
        public int OrderNo { get; set; }
    }
}
