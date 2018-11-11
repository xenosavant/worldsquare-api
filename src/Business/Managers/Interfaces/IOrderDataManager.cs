using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    interface IOrderDataManager
    {
        Task<Order> GetOrder(int id);
        Task<Order> UpdateOrder(Order order);
        Task<Order> FulfillOrderItems(int orderId, List<LineItem> items);
    }
}
