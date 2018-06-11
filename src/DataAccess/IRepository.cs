using Microsoft.EntityFrameworkCore.Storage;
using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.DataAccess
{
    public interface IRepository : IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity, int? createdBy = null)
            where TEntity : class, IAuditableEntity;

        void CreateRange<TEntity>(ICollection<TEntity> entities, int createdBy)
            where TEntity : class, IAuditableEntity;

        void TryUpdateManyToMany<TEntity, TKey>(IEnumerable<TEntity> currentItems, IEnumerable<TEntity> newItems, Func<TEntity, TKey> getKey)
            where TEntity : class, IAuditableEntity;

        IEnumerable<TEntity> Except<TEntity, TKey>(IEnumerable<TEntity> items, IEnumerable<TEntity> other, Func<TEntity, TKey> getKeyFunc)
            where TEntity : class, IAuditableEntity;

        void Update<TEntity>(TEntity entity, int? modifiedBy = null)
            where TEntity : class, IAuditableEntity;

        void Delete<TEntity>(TEntity entity, int? modifiedBy = null)
            where TEntity : class, IAuditableEntity;

        void Save();

        Task<int> SaveAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();

        void CreateSync<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IAuditableEntity;

        void UpdateSync<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IAuditableEntity;
    }
}
