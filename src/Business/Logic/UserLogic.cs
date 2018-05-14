﻿using AutoMapper;
using Stellmart.Api.DataAccess;
using Stellmart.Context.Entities;
using Stellmart.Data;
using Stellmart.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Business.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserLogic(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> SignupAsync(SignupRequest request)
        {
            _repository.Create(_mapper.Map<User>(request));
            return await _repository.SaveAsync();
        }

        public async Task<UserViewModel> GetByIdAsync(int id)
        {
            return _mapper.Map<UserViewModel>(await _repository.GetByIdAsync<User>(id));
        }

        public async Task<IReadOnlyCollection<UserViewModel>> GetAllAsync()
        {
            return _mapper.Map<List<UserViewModel>>(await _repository.GetAllAsync<User>());
        }
    }

    public interface IUserLogic
    {
        Task<int> SignupAsync(SignupRequest request);
        Task<IReadOnlyCollection<UserViewModel>> GetAllAsync();
        Task<UserViewModel> GetByIdAsync(int id);
    }
}
