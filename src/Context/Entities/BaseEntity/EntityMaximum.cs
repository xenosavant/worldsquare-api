namespace Stellmart.Api.Context.Entities.BaseEntity
{
    public class EntityMaximum : AuditableEntity<int>
    {
        public bool IsActive { get; set; }
    }
}
