using Stellmart.Data.Account;
using Stellmart.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IUserDataManager
    {
        Task<int> SignupAsync(SignupRequest request);
        Task<IReadOnlyCollection<ApplicationUserViewModel>> GetAllAsync();
        Task<ApplicationUserViewModel> GetByIdAsync(int id);
    }
}
