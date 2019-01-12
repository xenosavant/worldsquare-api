using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IOrderDataManager
    {
        Task<Order> GetOrder(int id, string navParams = null);
        Task<Order> UpdateOrder(Order order);
        Task<Order> CreateAsync(Order order);
    }
}
