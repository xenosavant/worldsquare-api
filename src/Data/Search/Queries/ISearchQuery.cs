using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Data.Search.Queries
{
    public interface ISearchQuery
    {
        string BuildAzureQueryFilter();
    }
}
