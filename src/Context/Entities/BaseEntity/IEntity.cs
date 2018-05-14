using System;

namespace Stellmart.Api.Context.Entities.BaseEntity
{
    public interface IEntity
    {
        object Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        string ModifiedBy { get; set; }
        bool IsDeleted { get; set; }
        string CreatedBy { get; set; }
        Guid UniqueId { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        new T Id { get; set; }
    }
}
