using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IThreadDataManager
    {
        Task<IEnumerable<MessageThread>> GetThreadsForListingAsync(int listingId);
        Task<IEnumerable<MessageThread>> GetThreadsForUserAsync(int userId);
        Task<MessageThread> GetById(int id);
        Task<MessageThread> CreateAndSaveAsync(MessageThread thread, int userId);
    }
}
