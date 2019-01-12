using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Search
{
    public class AzureSearchResult
    {
        public List<int> Ids { get; set; }
        public int Count { get; set; }
    }
}
