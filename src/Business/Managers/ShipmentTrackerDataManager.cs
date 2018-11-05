using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class ShipmentTrackerDataManager : IShipmentTrackerDataManager
    {
        private readonly IRepository _repository;

        public ShipmentTrackerDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<ShipmentTracker> CreateAsync(string secretKey, string trackingId, int signatureId)
        {
            var tracker = new ShipmentTracker()
            {
                SecretSigningKey = secretKey,
                TrackingId = trackingId,
                SignatureId = signatureId
            };
            _repository.Create(tracker);
            await _repository.SaveAsync();
            return tracker;
        }

        public Task<ShipmentTracker> GetAsync(int id)
        {
            return _repository.GetByIdAsync<ShipmentTracker>(id);
        }
    }
}
