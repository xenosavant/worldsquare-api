using Microsoft.EntityFrameworkCore.Storage;
using Stellmart.Api.Context.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.DataAccess
{
    public interface IRepository : IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IEntity;

        void CreateRange<TEntity>(ICollection<TEntity> entities, string createdBy = null)
            where TEntity : class, IEntity;

        void TryUpdateManyToMany<TEntity, TKey>(IEnumerable<TEntity> currentItems, IEnumerable<TEntity> newItems, Func<TEntity, TKey> getKey)
            where TEntity : class, IEntity;

        IEnumerable<TEntity> Except<TEntity, TKey>(IEnumerable<TEntity> items, IEnumerable<TEntity> other, Func<TEntity, TKey> getKeyFunc)
            where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity;

        void Delete<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity;

        void Save();

        Task<int> SaveAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();

        void CreateSync<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IEntity;

        void UpdateSync<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity;
    }
}
