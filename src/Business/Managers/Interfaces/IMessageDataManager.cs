using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Context.Entities;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IMessageDataManager
    {
        Task<Message> Update(Message message, int userId, bool save = false);
    }
}
