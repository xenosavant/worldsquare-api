using System;
using AutoMapper;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context;
using Stellmart.Api.Data;
using Stellmart.Api.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Enums;

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
            using (var transaction = await _repository.BeginTransactionAsync())
            {
                try
                {
                    var geolocation = new GeoLocation
                    {
                        Latitude = model.Latitude,
                        Longitude = model.Longtitude
                    };

                    _repository.Create(geolocation, userId);

                    var location = _mapper.Map<Location>(model);
                    location.GeoLocation = geolocation;
                    location.UserId = userId;

                    _repository.Create(location, userId);

                    await _repository.SaveAsync();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
            }
            
        }
    }
}
