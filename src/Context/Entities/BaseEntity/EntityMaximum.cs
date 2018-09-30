
namespace Stellmart.Api.Context.Entities.Entity
{
    public interface IEntityMaximum
    {
        bool IsActive { get; set; }
    }

    public class EntityMaximum : AuditableEntity<int>
    {
        public bool IsActive { get; set; }
    }
}
