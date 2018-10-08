using AutoMapper;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context;
using Stellmart.Api.Data;
using Stellmart.Api.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class LocationDataManager : ILocationDataManager
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public LocationDataManager(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<Location>> GetLocationsAsync(int userId)
        {
            return await _repository.GetAsync<Location>(x => x.UserId == userId) as IReadOnlyCollection<Location>;
        }

        public async Task CreateAsync(LocationModel model, int userId)
        {
            _repository.Create(_mapper.Map<Location>(model), userId);
            await _repository.SaveAsync();
        }
    }
}
