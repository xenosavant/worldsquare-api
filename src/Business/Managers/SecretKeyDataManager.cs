using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class SecretKeyDataManager : ISecretKeyDataManager
    {
        private readonly IRepository _repository;

        public SecretKeyDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<BuyerSecretKey> GetBuyerSecretKeyByOrderId(int orderId)
        {
            return (await _repository.GetAsync<BuyerSecretKey>(k => k.OrderId == orderId)).FirstOrDefault();
        }
    }
}
