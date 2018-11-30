using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class OrderItemDataManager : IOrderItemDataManager
    {

        private readonly IRepository _repository;

        public OrderItemDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public OrderItem Update(OrderItem item)
        {
            _repository.Update(item);
            return item;
        }
    }
}
