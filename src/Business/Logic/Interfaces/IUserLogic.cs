using Stellmart.Data;
using Stellmart.Data.Account;
using Stellmart.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Logic
{
    public interface IUserLogic
    {
        Task<int> SignupAsync(SignupRequest request);
        Task<IReadOnlyCollection<ApplicationUserViewModel>> GetAllAsync();
        Task<ApplicationUserViewModel> GetByIdAsync(int id);
    }
}
