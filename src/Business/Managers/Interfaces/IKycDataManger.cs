using Stellmart.Api.Context.Entities;
using Stellmart.Api.Data.Kyc;
using System.Threading.Tasks;

namespace Stellmart.Api.Business.Managers.Interfaces
{
    public interface IKycDataManager
    {
        Task CreateAsync(KycProfileModel model, int createdBy);
    }
}
