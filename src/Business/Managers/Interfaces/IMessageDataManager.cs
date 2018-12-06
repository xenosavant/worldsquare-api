using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Context.Entities;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IMessageDataManager
    {
        Task<Message> UpdateAsync(Message message, int userId, bool save = false);
        Task<Message> CreateAsync(Message message, int userId, bool save = false);
    }
}
