using Stellmart.Api.Business.Logic.Interfaces;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public class CartLogic : ICartLogic
    {
        private readonly ICartDataManager _dataManager;
        private readonly IInventoryItemDataManager _itemDataManager;

        public CartLogic(ICartDataManager dataManager, IInventoryItemDataManager itemDataManager)
        {
            _dataManager = dataManager;
            _itemDataManager = itemDataManager;
        }

        public async Task<Cart> AddItemToCart(InventoryItemDetailViewModel item, int userId)
        {
            var cart = await _dataManager.GetAsync(userId);
            var inventoryItem = await _itemDataManager.GetById(item.Id);
            if (cart == null)
            {
                cart = await _dataManager.CreateAsync(inventoryItem, userId);
            }
            else
            {
                cart.LineItems.Add(
                    new LineItem()
                    {
                        InventoryItemId = item.Id, 
                        Quantity = 1
                    });
                cart = await _dataManager.SaveAsync(cart);
            }
            return cart;
        }
    }
}
