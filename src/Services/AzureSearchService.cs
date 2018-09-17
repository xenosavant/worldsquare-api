using Stellmart.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Bounce.Api.Data.Indexes;
using Microsoft.Azure.Search.Models;
using Stellmart.Api.Context.Entities.Interfaces;
using Stellmart.Context;
using Microsoft.EntityFrameworkCore;
using Stellmart.Api.Business.Extensions;

namespace Stellmart.Api.Services
{
    public class AzureSearchService : ISearchService
    {
        private readonly ISearchServiceClient _serviceClient;

        public AzureSearchService(ISearchServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task Index<TEntity, TIndex>(List<TIndex> documents) 
            where TEntity : ISearchable 
            where TIndex : class
        {
            var indexClient = GetClient(typeof(TIndex).Name);
            var batch = IndexBatch.Upload(documents);
            var result = await indexClient.Documents.IndexAsync(batch);
        }

        public async Task Update<TEntity, TIndex>(List<TIndex> documents)
            where TEntity : ISearchable
            where TIndex : class
        {
            var indexClient = GetClient(typeof(TIndex).Name);
            var batch = IndexBatch.Merge(documents);
            var result = await indexClient.Documents.IndexAsync(batch);
        }

        public async Task Delete<TEntity, TIndex>(List<TIndex> documents)
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
