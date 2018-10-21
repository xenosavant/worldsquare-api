using System.Threading.Tasks;

namespace Stellmart.Api.Services.Interfaces
{
    public interface IAzureStorageService
    {
        Task<string> DownloadTextAsync(string filename, string containerName);
    }
}
