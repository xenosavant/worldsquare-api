using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Context.Entities.Interfaces
{
    public interface ISearchable
    {
        IList<Field> GetFields();
    }
}
