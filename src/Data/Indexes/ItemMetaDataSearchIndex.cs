using System;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Spatial;
using Newtonsoft.Json;

namespace Bounce.Api.Data.Indexes
{
    [SerializePropertyNamesAsCamelCase]
    public partial class ItemMetaDataSearchIndex
    {
        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable]
        public string ListingId { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        public string ItemConditionId { get; set; }

        [IsSearchable, IsFilterable, IsFacetable]
        public string[] KeyWords { get; set; }

        [IsSearchable, IsFilterable]
        public string[] CategoryIds { get; set; }

    }
}
