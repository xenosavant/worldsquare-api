using System.Collections.Generic;
using System.Threading.Tasks;
using Stellmart.Api.Data;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public interface ILocationLogic
    {
        Task<IReadOnlyCollection<LocationModel>> GetLocationsAsync(int userId);
        Task CreateAsync(LocationModel model, int userId);
    }
}
