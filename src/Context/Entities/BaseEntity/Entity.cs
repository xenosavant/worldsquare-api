using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stellmart.Api.Context.Entities.BaseEntity
{
    public interface IEntity
    {
        object Id { get; set; }
        Guid UniqueId { get; set; }
        bool IsDeleted { get; set; }
    }
    
    public interface IEntity<T> : IEntity
    {
        new T Id { get; set; }
    }

    public abstract class Entity<T> : IEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        object IEntity.Id
        {
            get => Id;
            set => throw new NotImplementedException();
        }

        public Guid UniqueId { get; set; }

        public bool IsDeleted { get; set; }

    }
}
