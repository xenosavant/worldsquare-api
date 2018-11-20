using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Shipping;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class ProductShipmentDataManager : IProductShipmentDataManager
    {
        private readonly IRepository _repository;

        public ProductShipmentDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductShipment> CreateAsync(ShipPackageData data)
        {
            var shipment = new ProductShipment()
            {
                OrderId = data.OrderId,
                ShippingCarrierType = data.ShippingCarrierType,
                TrackingNumber = data.TrackingId,
                Items = data.Items
            };
            _repository.Create(shipment);
            await _repository.SaveAsync();
            return await _repository.GetOneAsync<ProductShipment>(p => p.Id == shipment.Id, "Items.Contract");
        }
    }
}
