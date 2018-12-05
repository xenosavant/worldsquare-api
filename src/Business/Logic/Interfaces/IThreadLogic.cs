﻿using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic.Interfaces
{
    public interface IThreadLogic
    {
        Task<IEnumerable<MessageThread>> GetAsync(
            int? listingId,
            int? page,
            int? pageLength);
    }
}
