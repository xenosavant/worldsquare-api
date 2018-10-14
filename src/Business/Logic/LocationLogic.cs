using AutoMapper;
using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public class LocationLogic : ILocationLogic
    {
        private readonly ILocationDataManager _locationDataManager;
        private readonly IMapper _mapper;

        public LocationLogic(ILocationDataManager locationDataManager, IMapper mapper)
        {
            _locationDataManager = locationDataManager;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<LocationModel>> GetLocationsAsync(int userId)
        {
            return _mapper.Map<IReadOnlyCollection<LocationModel>>(await _locationDataManager.GetLocationsAsync(userId));
        }

        public async Task CreateAsync(LocationModel model, int userId)
        {
            await _locationDataManager.CreateAsync(model, userId);
        }

        public async Task<int> SetDefaultAsync(LocationModel model, int userId)
        {
            return await _locationDataManager.SetDefaultAsync(model, userId);
        }
    }
}
