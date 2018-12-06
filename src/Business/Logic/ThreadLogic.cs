using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public class ThreadLogic : IThreadLogic
    {
        private readonly IThreadDataManager _threadManager;
        private readonly IMessageDataManager _messageManager;

        public ThreadLogic(IThreadDataManager threadManager, IMessageDataManager messageManager)
        {
            _threadManager = threadManager;
            _messageManager = messageManager;
        }

        public async Task<IEnumerable<MessageThread>> GetAsync(
            int? userId,
            int? listingId,
            int? page,
            int? pageLength)
        {
            if (listingId != null)
            {
                return await _threadManager.GetThreadsForListingAsync((int)listingId);
            }
            else
            {
                return await _threadManager.GetThreadsForUserAsync((int)userId);
            }
        }

        public async Task<MessageThread> PostMessageToThread(Message message, int threadId, int userId)
        {
            message.MessageThreadId = threadId;
            await _messageManager.CreateAsync(message, userId, true);
            return await _threadManager.GetById(threadId);
        }
    }
}
