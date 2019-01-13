using Stellmart.Api.Context;
using Stellmart.Api.Data.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IUserDataManager
    {
        Task<bool> SignupAsync(ApplicationUserModel model);
        Task<IReadOnlyCollection<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(int id, string navParams = null);
    }
}