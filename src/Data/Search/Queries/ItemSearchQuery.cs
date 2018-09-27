using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Search.Models;

namespace Stellmart.Api.Data.Search.Queries
{
    public class ItemSearchQuery : ISearchQuery
    {
        public int? ItemConditionId { get; set; }
        public int? ParentId { get; set; }
        public string Category { get; set; }
        public double? MinimumPriceUsd { get; set; }
        public double? MaximumPriceUsd { get; set; }
        public double? MinumumPriceXlm { get; set; }
        public double? MaximumPriceXlm { get; set; }


        public string BuildAzureQueryFilter()
        {
            var builder = new StringBuilder();
            if (ParentId != null)
            {
                builder.Append("parentId eq ");
                builder.Append(ParentId);
            }
            if (ItemConditionId != null)
            {
                if (builder.Length != 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("itemConditionId eq ");
                builder.Append(ItemConditionId.ToString());
            }
            if (Category != null)
            {
                if (builder.Length != 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("categories/any(c: c eq '");
                builder.Append(Category);
                builder.Append("'");
            }
            if (MinimumPriceUsd != null && MaximumPriceUsd != null)
            {
                if (builder.Length != 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("usdPrice ge ");
                builder.Append(MinimumPriceUsd);
                builder.Append(" and usdPrice le ");
                builder.Append(MaximumPriceUsd);
            }
            if (MinumumPriceXlm != null && MaximumPriceXlm != null)
            {
                if (builder.Length != 0)
                {
                    builder.Append(" and ");
                }
                builder.Append("xlmPrice ge ");
                builder.Append(MinumumPriceXlm);
                builder.Append(" and xlmPrice le ");
                builder.Append(MaximumPriceXlm);
            }
            var query = builder.ToString();
            if (String.IsNullOrEmpty(query))
            {
                return null;
            }
            else return query;
        }
    }
}
