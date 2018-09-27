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
            var indexClient = GetClient(typeof(TIndex).Name);
            var searchParams = new SearchParameters()
            {
                Filter = queryFilter.BuildAzureQueryFilter(),
                Select = new[] { "Id" }
            };
            var results = await indexClient.Documents.SearchAsync(searchText == null ? "*" : searchText, searchParams);
            return results.Results.Select(r =>
            {
                object id = 0;
                r.Document.TryGetValue("Id", out id);
                return (int)id;
            }).ToList();
        }


        public async Task IndexAsync<TEntity, TIndex>(List<TIndex> documents) 
            where TEntity : ISearchable 
            where TIndex : class
        {
            var indexClient = GetClient(typeof(TIndex).Name);
            var batch = IndexBatch.Upload(documents);
            var result = await indexClient.Documents.IndexAsync(batch);
        }

        public async Task UpdateAsync<TEntity, TIndex>(List<TIndex> documents)
            where TEntity : ISearchable
            where TIndex : class
        {
            var indexClient = GetClient(typeof(TIndex).Name);
            var batch = IndexBatch.MergeOrUpload(documents);
            var result = await indexClient.Documents.IndexAsync(batch);
        }

        public async Task DeleteAsync<TEntity, TIndex>(List<TIndex> documents)
            where TEntity : ISearchable
            where TIndex : class
        {
            var indexClient = GetClient(typeof(TIndex).Name);
            var batch = IndexBatch.Delete(documents);
            var result = await indexClient.Documents.IndexAsync(batch);
        }

        private ISearchIndexClient GetClient(string indexName)
        {
            return _serviceClient.Indexes.GetClient(indexName.ToLower());
        }
    }
}
