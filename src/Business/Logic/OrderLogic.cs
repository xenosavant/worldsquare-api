using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderDataManager _orderDataManager;

        public OrderLogic(IOrderDataManager orderDataManager)
        {
            _orderDataManager = orderDataManager;
        }

    }
}
