using Microsoft.EntityFrameworkCore;
using Stellmart.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Stellmart.DataAccess
{
    public class GenericDataAccess<T> : IGenericDataAccess<T> where T : class, IStellmartEntity
    {
        private readonly StellmartContext _context;

        public GenericDataAccess(StellmartContext context)
        {
            _context = context;
        }

        public async virtual Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            IQueryable<T> dbQuery = _context.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = await dbQuery
                .AsNoTracking()
                .ToListAsync<T>();

            return list;
        }

        public async virtual Task<IList<T>> GetListAsync(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = await dbQuery
                .AsNoTracking()
                .Where(where)
                .AsQueryable<T>()
                .ToListAsync();

            return list;
        }

        public async virtual Task<T> GetSingleAsync(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item;
            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = await dbQuery
                .AsNoTracking()
                .Where(where)
                .AsQueryable<T>()
                .FirstOrDefaultAsync();

            return item;
        }

        public async virtual Task<T> GetSingleByIdAsync(int id,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item;
            IQueryable<T> dbQuery = _context.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = await dbQuery
                .AsNoTracking()
                .Where(u => u.Id == id)
                .AsQueryable<T>()
                .FirstOrDefaultAsync();

            return item;
        }

        public async Task<T> InsertAsync(T item)
        {
            _context.Set<T>().Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }

    public interface IGenericDataAccess<T>
    {
        Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties);
        Task<IList<T>> GetListAsync(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        Task<T> GetSingleAsync(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        Task<T> GetSingleByIdAsync(int id, params Expression<Func<T, object>>[] navigationProperties);
        Task<T> InsertAsync(T item);
    }

}