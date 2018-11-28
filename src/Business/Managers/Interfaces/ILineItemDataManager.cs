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
        Task<LineItem> UpdateAndSaveAsync(LineItem item, Delta<LineItem> delta);
        void Update(LineItem item);
        Task DeleteAsync(LineItem item);
    }
}
