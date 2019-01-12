using System;
using System.Linq;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Spatial;
using Newtonsoft.Json;
using Stellmart.Api.Context.Entities;

namespace Bounce.Api.Data.Search.Indexes
{
    [SerializePropertyNamesAsCamelCase]
    public partial class ItemMetaDataSearchIndex
    {
        public ItemMetaDataSearchIndex(Listing listing)
        {
            Id = listing.Id.ToString();
            ParentId = listing.ServiceId;
            Title = listing.Title;
            ItemConditionId = listing.ItemMetaData.ItemConditionId;
            KeyWords = JsonConvert.DeserializeObject<string[]>(listing.ItemMetaData.KeyWords);
            Categories = listing.ItemMetaData.Categories.Select(c => c.Description).ToArray();
            MinUsdPrice = listing.GetMinimumPrice("USD");
            MaxUsdPrice = listing.GetMaximumPrice("USD");
            MinXlmPrice = listing.GetMinimumPrice("XLM");
            MaxXlmPrice = listing.GetMaximumPrice("XLM");
        }

        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable]
        public string Id { get; set; }

        [IsFilterable]
        public int ParentId { get; set; }

        [IsSearchable]
        public string Title { get; set; }

        [IsSearchable]
        public string[] KeyWords { get; set; }

        [IsFilterable, IsFacetable]
        public int ItemConditionId { get; set; }

        [IsFilterable, IsFacetable]
        public string [] Categories { get; set; }

        [IsFilterable]
        public int? MinUsdPrice { get; set; }

        [IsFilterable]
        public int? MaxUsdPrice { get; set; }

        [IsFilterable]
        public int? MinXlmPrice { get; set; }

        [IsFilterable]
        public int? MaxXlmPrice { get; set; }

    }
}
