﻿using Microsoft.AspNetCore.Mvc;
using Stellmart.Api.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stellmart.Api.Data.ViewModels;
using Stellmart.Api.Data;

namespace Stellmart.Api.Business.Logic
{
    public interface IOnlineStoreLogic
    {
        Task<IEnumerable<OnlineStore>> GetAll();
        Task<OnlineStore> GetById(int id);
        Task<OnlineStore> Create(int userId, OnlineStoreViewModel store);
        Task<OnlineStore> Update(int userId, OnlineStore store, Delta<OnlineStore> delta);
        Task Delete(OnlineStore store);
    }
}
