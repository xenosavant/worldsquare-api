using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class OrderDataManager : IOrderDataManager
    {

        private readonly IRepository _repository;

        public OrderDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public Task<Order> GetOrder(int id)
        {
            return _repository.GetByIdAsync<Order>(id);
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            _repository.Update(order);
            await _repository.SaveAsync();
            return order;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _repository.Create(order);
            await _repository.SaveAsync();
            return order;
        }
    }
}
