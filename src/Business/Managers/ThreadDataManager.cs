using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class ThreadDataManager : IThreadDataManager
    {
        private readonly IRepository _repository;
        private readonly IMessageDataManager _messageManager;
        private readonly string _navigationProperties = "Messages,Listing,MessageThreadMembers";

        public ThreadDataManager(IRepository repository, IMessageDataManager messageManager)
        {
            _messageManager = messageManager;
            _repository = repository;
        }

        public Task<IEnumerable<MessageThread>> GetThreadsForListingAsync(int listingId)
        {
            return _repository.GetAsync<MessageThread>(r => r.ListingId == listingId, null, _navigationProperties);
        }

        public Task<IEnumerable<MessageThread>> GetThreadsForUserAsync(int userId)
        {
            return _repository.GetAsync<MessageThread>(r => r.MessageThreadMembers.Select(m => m.UserId).Contains(userId), null, _navigationProperties);
        }

        public Task<MessageThread> GetById(int id)
        {
            return _repository.GetOneAsync<MessageThread>(s => s.Id == id, _navigationProperties);
        }

        public async Task<MessageThread> CreateAndSaveAsync(MessageThread thread, int userId)
        {
            _messageManager.Update(thread.Messages.First(), userId);
            _repository.Create(thread, userId);
            await _repository.SaveAsync();
            return await GetById(thread.Id);
        }
    }
}
