using AutoMapper;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> SetDefaultAsync(LocationModel model, int userId)
        {
            var locations = await _repository.GetAsync<Location>(x => x.UserId == userId) as List<Location>;
        
            // check if address ID exist in current users locations
            // (maybe this is not the most elegant option against fraudulent request ?)
            // so user can't get address of someone else by updating his own list
            if (locations.Any(x => x.Id == model.Id))
            {
                // this makes multiple update queries, there is no really efficient way in EF to do this.
                // we should think of moving to dapper and ado.net in future
                locations.ForEach(x => x.IsDefault = false);
                var location = locations.FirstOrDefault(x => x.Id == model.Id);

                if (location != null)
                {
                    location.IsDefault = true;
                }

                return await _repository.SaveAsync();
            }

            return 400;
        }
    }
}
