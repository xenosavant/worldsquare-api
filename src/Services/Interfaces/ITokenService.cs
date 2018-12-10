using System.Threading.Tasks;
using Stellmart.Api.Data.Horizon;

namespace Stellmart.Api.Services.Interfaces
{
    public interface ITokenService
    {
        Task<HorizonTokenModel> CreateAsset(string name, string limit);
        Task<bool> MoveAssetToDistributor(HorizonTokenModel token);
        Task<bool> LockIssuer(HorizonTokenModel token);

    }
}