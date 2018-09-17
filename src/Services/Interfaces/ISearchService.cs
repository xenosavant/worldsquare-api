using Stellmart.Api.Context.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface ISearchService
    {
        Task Index<TEntity, TIndex>(List<TIndex> documents)
           where TEntity : ISearchable
           where TIndex : class;

        Task Update<TEntity, TIndex>(List<TIndex> documents)
            where TEntity : ISearchable
            where TIndex : class;

        Task Delete<TEntity, TIndex>(List<TIndex> documents)
            where TEntity : ISearchable
            where TIndex : class;
    }
}
