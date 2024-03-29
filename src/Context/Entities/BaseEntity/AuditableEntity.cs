﻿using Stellmart.Api.Context.Entities.BaseEntity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Stellmart.Api.Context.Entities.Entity
{

    public interface IAuditableEntity : IEntity, IUniqueEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        int? ModifiedBy { get; set; }
        int CreatedBy { get; set; }
    }

    public class AuditableEntity<T> : UniqueEntity, IAuditableEntity
    {
        private DateTime? _createdDate;
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate
        {
            get => _createdDate ?? DateTime.UtcNow;
            set => _createdDate = value;
        }

        public int CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }
    }
}
