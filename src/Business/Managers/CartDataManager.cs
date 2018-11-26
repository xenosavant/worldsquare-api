using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class CartDataManager : ICartDataManager
    {
        private readonly IRepository _repository;

        public CartDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Cart> GetAsync(int userId)
        {
            var cart = await _repository.GetOneAsync<Cart>(c => c.UserId == userId, "LineItems.InventoryItem.Listing.ItemMetaData");
            cart.LineItems = cart.LineItems.Where(l => !l.IsDeleted).ToList();
            return cart;
        }

        public async Task<Cart> CreateAsync(InventoryItem item, int userId)
        {
            var cart = new Cart()
            {
                UserId = userId,
                LineItems = new List<LineItem>()
                {
                    new LineItem()
                    {
                        InventoryItem = item,
                        Quantity = 1
                    }
                }
            };
            _repository.Create(cart);
            await _repository.SaveAsync();
            return cart;
        }

        public async Task<Cart> SaveAsync(Cart cart)
        {
            _repository.Create(cart);
            await _repository.SaveAsync();
            return await GetAsync(cart.UserId);
        }
    }
}
