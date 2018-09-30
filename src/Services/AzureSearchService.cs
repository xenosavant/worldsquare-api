using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Stellmart.Api.Context.Entities.Interfaces;
using Stellmart.Context;
using Microsoft.EntityFrameworkCore;
using Stellmart.Api.Business.Extensions;
using Stellmart.Api.Data.Search.Queries;
using Newtonsoft.Json;

namespace Stellmart.Api.Services
{
    public class AzureSearchService : ISearchService
    {
        private readonly ISearchServiceClient _serviceClient;

        public AzureSearchService(ISearchServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<List<int>> SearchAsync<TEntity, TIndex>(string searchText, ISearchQuery queryFilter)
        {
            var indexClient = GetClient(typeof(TEntity).Name.ToLower());
            var searchParams = new SearchParameters()
            {
                Filter = queryFilter.BuildAzureQueryFilter(),
                Select = new[] { "id" }
            };
            // TODO: Fuzzy search isn't working
            var results = await indexClient.Documents.SearchAsync(searchText == null ? "*" : searchText + "~1", searchParams);
            return results.Results.Select(r =>
            {
                r.Document.TryGetValue("id", out object id);
                Int32.TryParse((string)id, out int result);
                return result;
            }).ToList();
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
