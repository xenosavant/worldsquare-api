using System.Collections.Generic;
using System.Threading.Tasks;
using Stellmart.Api.Context;
using Stellmart.Api.Data;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface ILocationDataManager
    {
        Task<IReadOnlyCollection<Location>> GetLocationsAsync(int userId);
        Task CreateAsync(LocationModel model, int userId);
    }
}
