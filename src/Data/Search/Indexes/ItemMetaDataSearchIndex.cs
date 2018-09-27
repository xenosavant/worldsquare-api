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
            ParentId = listing.OnlineStore.Id;
            Title = listing.Title;
            ItemConditionId = listing.ItemMetaData.ItemConditionId;
            KeyWords = JsonConvert.DeserializeObject<string[]>(listing.ItemMetaData.KeyWords);
            Categories = listing.ItemMetaData.Categories.Select(c => c.Description).ToArray();
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
        public double? UsdPrice { get; set; }

        [IsFilterable]
        public double? XlmPrice { get; set; }

    }
}
