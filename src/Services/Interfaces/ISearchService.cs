using Stellmart.Api.Context.Entities.Interfaces;
using Stellmart.Api.Data.Search.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface ISearchService
    {
        Task<List<int>> SearchAsync<TEntity, TIndex>(string searchText, ISearchQuery queryFilter);

        Task IndexAsync<TEntity, TIndex>(List<TIndex> documents)
           where TEntity : ISearchable
           where TIndex : class;

        Task UpdateAsync<TEntity, TIndex>(List<TIndex> documents)
            where TEntity : ISearchable
            where TIndex : class;

        Task DeleteAsync<TEntity, TIndex>(List<TIndex> documents)
            where TEntity : ISearchable
            where TIndex : class;
    }
}
