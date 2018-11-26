using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class LineItemDataManager : ILineItemDataManager
    {
        private readonly IRepository _repository;

        public LineItemDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<LineItem> GetAsync(int id)
        {
            return await _repository.GetOneAsync<LineItem>(l => l.Id == id, "Cart");
        }

        public async Task<LineItem> UpdateAsync(LineItem item, Delta<LineItem> delta)
        {
            delta.Patch(item);
            _repository.Update(item);
            await _repository.SaveAsync();
            return item;
        }

        public async Task DeleteAsync(LineItem item)
        {
            item.IsDeleted = true;
            _repository.Update(item);
            await _repository.SaveAsync();
        }
    }
}
