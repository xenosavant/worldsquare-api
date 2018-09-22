using Stellmart.Api.Context.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface ICountryDataManager
    {
        Task<IReadOnlyCollection<Country>> GetAllAsync();
        Task<Country> GetByIsoAsync(string countryIso);
    }
}
