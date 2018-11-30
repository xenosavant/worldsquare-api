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
        private readonly string navigationProperties = "LineItems.InventoryItem.Listing.ItemMetaData";

        public CartDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Cart> GetAsync(int userId, string properties = null)
        {
            var cart = await _repository.GetOneAsync<Cart>(c => c.UserId == userId, properties ?? navigationProperties);
            if (cart != null)
            {
                cart.LineItems = cart.LineItems.Where(l => !l.IsDeleted).ToList();
            }
            return cart;
        }

        public async Task<Cart> CreateAsync(InventoryItem item, int userId)
        {
            LineItem lineItem = null;
            if (item != null)
            {
                lineItem = new LineItem()
                {
                    InventoryItem = item,
                    Quantity = 1
                };
            }
            var cart = new Cart()
            {
                UserId = userId,
                LineItems = lineItem == null ? null : 
                new List<LineItem>()
                {
                    lineItem
                }
            };
            _repository.Create(cart);
            await _repository.SaveAsync();
            return cart;
        }

        public async Task<Cart> SaveAsync(Cart cart)
        {
            _repository.Update(cart);
            await _repository.SaveAsync();
            cart  = await GetAsync(cart.UserId);
            return cart;
        }
    }
}
