﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stellmart.Api.Context.Entities.Entity
{
    public interface IEntity
    {
        object Id { get; }
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
        }

        public Guid UniqueId { get; set; }

        public bool IsDeleted { get; set; }

    }
}
