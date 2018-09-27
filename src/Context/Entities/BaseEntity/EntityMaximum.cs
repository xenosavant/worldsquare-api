
namespace Stellmart.Api.Context.Entities.Entity
{
    public interface IEntityMaximum
    {
        bool IsActive { get; set; }
    }

    public class EntityMaximum : UniqueEntity<int>
    {
        public bool IsActive { get; set; }
    }
}
