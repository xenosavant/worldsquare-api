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
        private readonly ILineItemDataManager _lineItemDataManager;

        public CartLogic(ICartDataManager dataManager, 
            IInventoryItemDataManager itemDataManager, 
            ILineItemDataManager lineItemDataManager)
        {
            _dataManager = dataManager;
            _itemDataManager = itemDataManager;
            _lineItemDataManager = lineItemDataManager;
        }

        public async Task<Cart> AddItemToCart(InventoryItem item, int userId)
        {
            var cart = await _dataManager.GetAsync(userId);
            if (cart == null)
            {
                cart = await _dataManager.CreateAsync(item, userId);
            }
            else
            {
                if (cart.LineItems.Select(li => li.InventoryItemId).Contains(item.Id))
                {
                    var lineItem = cart.LineItems.Where(li => li.InventoryItemId == item.Id).First();
                    lineItem.Quantity++;
                    _lineItemDataManager.Update(lineItem);
                }
                else
                {
                    cart.LineItems.Add(
                        new LineItem()
                        {
                            InventoryItemId = item.Id,
                            Quantity = 1
                        });
                }
                cart = await _dataManager.SaveAsync(cart);
            }
            return cart;
        }
    }
}
