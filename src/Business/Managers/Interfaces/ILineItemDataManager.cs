using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface ILineItemDataManager
    {
        Task<LineItem> GetAsync(int id);
        Task<LineItem> UpdateAsync(LineItem item, Delta<LineItem> delta);
        Task DeleteAsync(LineItem item);
    }
}
