﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Stellmart.Api.Context.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.DataAccess
{
    public class Repository<TContext> : ReadOnlyRepository<TContext>, IRepository
            where TContext : DbContext
    {
        public Repository(TContext context)
            : base(context)
        {
        }

        public virtual void Create<TEntity>(TEntity entity, int? createdBy = null)
            where TEntity : class, IAuditableEntity
        {
            entity.CreatedDate = DateTime.UtcNow;

            if (createdBy.HasValue && createdBy.Value > 0)
            {
                entity.CreatedBy = (int)createdBy;
            }

            if (entity.UniqueId == new Guid())
            {
                entity.UniqueId = Guid.NewGuid();
            }

            context.Set<TEntity>().Add(entity);
        }

        public virtual void CreateRange<TEntity>(ICollection<TEntity> entities, int createdBy)
            where TEntity : class, IAuditableEntity
        {
            foreach (var item in entities)
            {
                item.CreatedDate = DateTime.UtcNow;
                item.CreatedBy = createdBy;

                if (item.UniqueId == new Guid())
                {
                    item.UniqueId = Guid.NewGuid();
                }
            }

            context.Set<TEntity>().AddRange(entities);
        }

        public virtual void TryUpdateManyToMany<TEntity, TKey>(IEnumerable<TEntity> currentItems, IEnumerable<TEntity> newItems, Func<TEntity, TKey> getKey) where TEntity : class, IAuditableEntity
        {
            context.Set<TEntity>().RemoveRange(Except(currentItems, newItems, getKey));
            context.Set<TEntity>().AddRange(Except(newItems, currentItems, getKey));
        }

        public virtual IEnumerable<TEntity> Except<TEntity, TKey>(IEnumerable<TEntity> items, IEnumerable<TEntity> other, Func<TEntity, TKey> getKeyFunc) where TEntity : class, IAuditableEntity
        {
            return items
                .GroupJoin(other, getKeyFunc, getKeyFunc, (item, tempItems) => new { item, tempItems })
                .SelectMany(t => t.tempItems.DefaultIfEmpty(), (t, temp) => new { t, temp })
                .Where(t => ReferenceEquals(null, t.temp) || t.temp.Equals(default(TEntity)))
                .Select(t => t.t.item);
        }

        public virtual void Update<TEntity>(TEntity entity, int? modifiedBy)
            where TEntity : class, IAuditableEntity
        {
            entity.ModifiedDate = DateTime.UtcNow;
            entity.ModifiedBy = modifiedBy;
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete<TEntity>(TEntity entity, int? modifiedBy = null)
            where TEntity : class, IAuditableEntity
        {
            entity.ModifiedDate = DateTime.UtcNow;
            entity.ModifiedBy = modifiedBy;
            entity.IsDeleted = true;
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            context.SaveChanges();
        }

        public virtual Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }

        public virtual Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return context.Database.BeginTransactionAsync();
        }

        public virtual void CreateSync<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IAuditableEntity
        {
            context.Set<TEntity>().Add(entity);
        }

        public virtual void UpdateSync<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IAuditableEntity
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
