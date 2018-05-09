using Stellmart.DataAccess;
using Stellmart.Data;
using Stellmart.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Stellmart.Business.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDataAccess _dataAccess;

        public UserLogic(IUserDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<User> Signup(SignupRequest request)
        {
            var user = new User()
            {
                Email = request.Email,
                Username = request.Username,
            };
            return await _dataAccess.InsertAsync(user);
        }

        public async Task<User> Get(int id)
        {
            return await _dataAccess.GetSingleByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dataAccess.GetAllAsync();
        }
    }

    public interface IUserLogic
    {
        Task<User> Signup(SignupRequest reqest);
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(int id);
    }
}
