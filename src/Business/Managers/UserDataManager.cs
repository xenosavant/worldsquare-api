﻿using AutoMapper;
using Stellmart.Api.Business.Managers.Interfaces;
using Stellmart.Api.Context;
using Stellmart.Api.DataAccess;
using Stellmart.Data.Account;
using Stellmart.Data.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers
{
    public class UserDataManager : IUserDataManager
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserDataManager(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> SignupAsync(SignupRequest request)
        {
            _repository.Create(_mapper.Map<ApplicationUser>(request));
            return await _repository.SaveAsync();
        }

        public async Task<ApplicationUserViewModel> GetByIdAsync(int id)
        {
            return _mapper.Map<ApplicationUserViewModel>(await _repository.GetByIdAsync<ApplicationUser>(id));
        }

        public async Task<IReadOnlyCollection<ApplicationUserViewModel>> GetAllAsync()
        {
            return _mapper.Map<List<ApplicationUserViewModel>>(await _repository.GetAllAsync<ApplicationUser>());
        }

        //example method for ordering and including entities by eager loading (aka include())
        public async Task<IReadOnlyCollection<ApplicationUserViewModel>> GetAllOrderingAndIncludeExampleAsync()
        {
            return _mapper.Map<List<ApplicationUserViewModel>>(await _repository.GetAsync<ApplicationUser>(x => x.IsActive, x => x.OrderByDescending(y => y.CreatedDate), "NameOfRelatedEntity,NameOfOtherRelatedEntity,NameOfRelatedEntity.ChildEntity"));
        }
    }
}