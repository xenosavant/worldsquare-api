using Stellmart.Api.Context.Entities.Entity;
using Stellmart.Api.Context.Entities.ReadOnly;

namespace Stellmart.Api.Context.Entities
{
    public class SecurityAnswer : AuditableEntity<int>
    {
        public int Order { get; set; }
        public string Answer { get; set; }
        public SecurityQuestion SecurityQuestion { get; set; }
        public ApplicationUser User { get; set; }
    }
}
