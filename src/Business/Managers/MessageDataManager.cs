using Stellmart.Api.Business.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Context.Entities;
using Stellmart.Api.DataAccess;

namespace Stellmart.Api.Business.Managers
{
    public class MessageDataManager : IMessageDataManager
    {
        private readonly IRepository _repository;

        public MessageDataManager(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Message> UpdateAsync(Message message, int userId, bool save = false)
        {
            _repository.Update(message, userId);
            if (save)
            {
                await _repository.SaveAsync();
            }
            return message;
        }

        public async Task<Message> CreateAsync(Message message, int userId, bool save = false)
        {
            _repository.Create(message, userId);
            if (save)
            {
                await _repository.SaveAsync();
            }
            return message;
        }
    }
}
