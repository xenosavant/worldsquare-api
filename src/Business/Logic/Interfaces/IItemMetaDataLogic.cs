using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public interface IItemMetaDataLogic
    {
        Task<ItemMetaData> GetById(int id, string navigationProperties = null);
        Task<ItemMetaData> UpdateAndSaveAsync(int userId, ItemMetaData metaData, Delta<ItemMetaData> delta);
    }
}
