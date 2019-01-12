using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Stellmart.Api.Context.Entities.Interfaces;
using Stellmart.Api.Data.Search;
using Stellmart.Api.Data.Search.Queries;
using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Services
{
    public class AzureSearchService : ISearchService
    {
        private readonly ISearchServiceClient _serviceClient;

        public AzureSearchService(ISearchServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<AzureSearchResult> SearchAsync<TEntity, TIndex>(string searchText, ISearchQuery queryFilter, int skip, int take)
        {
            var indexClient = GetClient(typeof(TEntity).Name.ToLower());

            var searchParams = new SearchParameters()
            {
                Filter = queryFilter.BuildAzureQueryFilter(),
                Select = new[] { "id" },
                QueryType = QueryType.Full,
                SearchMode = SearchMode.Any,
                Skip = skip,
                Top = take,
                IncludeTotalResultCount = true
            };

            var splitText = searchText.Split("+");
            if (splitText.Count() > 1)
            {
                searchText = splitText.Aggregate((accumulator, value) => accumulator += value + "~ ");
            }
            var result = await indexClient.Documents.SearchAsync(searchText == null ? "*" : searchText, searchParams);
            var ids =  result.Results.Select(r =>
            {
                r.Document.TryGetValue("id", out object id);
                Int32.TryParse((string)id, out int returnId);
                return returnId;
            }).ToList();
            return new AzureSearchResult()
            {
                Ids = ids,
                Count = result.Results.Count
            };
        }


        public async Task IndexAsync<TEntity, TIndex>(List<TIndex> documents) 
            where TEntity : ISearchable 
            where TIndex : class
        {
            var indexClient = GetClient(typeof(TEntity).Name.ToLower());
            var batch = IndexBatch.Upload(documents);
            var result = await indexClient.Documents.IndexAsync(batch);
        }

        public async Task UpdateAsync<TEntity, TIndex>(List<TIndex> documents)
            where TEntity : ISearchable
            where TIndex : class
        {
            var indexClient = GetClient(typeof(TEntity).Name.ToLower());
            var batch = IndexBatch.MergeOrUpload(documents);
            var result = await indexClient.Documents.IndexAsync(batch);
        }

        public async Task DeleteAsync<TEntity, TIndex>(List<TIndex> documents)
            where TEntity : ISearchable
            where TIndex : class
        {
            var indexClient = GetClient(typeof(TEntity).Name.ToLower());
            var batch = IndexBatch.Delete(documents);
            var result = await indexClient.Documents.IndexAsync(batch);
        }

        private ISearchIndexClient GetClient(string indexName)
        {
            return _serviceClient.Indexes.GetClient(indexName.ToLower());
        }
    }
}
