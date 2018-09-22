using AutoMapper;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class CountryDataManager : ICountryDataManager
    {
        private readonly IRepository _repository;

        public CountryDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<Country>> GetAllAsync()
        {
            return await _repository.MinimalGetAllAsync<Country>() as IReadOnlyCollection<Country>;
        }

        public async Task<Country> GetByIsoAsync(string countryIso)
        {
            return await _repository.MinimalGetOneAsync<Country>(x => x.Code == countryIso);
        }
    }
}
