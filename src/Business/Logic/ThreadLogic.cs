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

        public ThreadLogic(IThreadDataManager threadManager)
        {
            _threadManager = threadManager;
        }

        public async Task<IEnumerable<MessageThread>> GetAsync(
            int? listingId,
            int? page,
            int? pageLength)
        {
            throw new NotImplementedException();
        }
    }
}
