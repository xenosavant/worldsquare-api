using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Stellmart.Api.Data.Settings;
using Stellmart.Api.Services.Interfaces;

namespace Stellmart.Api.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private const string ContainerName = "cdn";
        private readonly CloudBlobClient _client;
        private readonly Lazy<CloudBlobContainer> _container;

        private readonly IOptions<AzureSettings> _azureSettings;

        public AzureStorageService(IOptions<AzureSettings> azureSettings)
        {
            _azureSettings = azureSettings;
            _client = CreateCloudBlobClient();
            _container = new Lazy<CloudBlobContainer>(() => {
                var containerReference = _client.GetContainerReference(ContainerName);
                containerReference.CreateIfNotExistsAsync();
                return containerReference;
            });
        }

        private CloudBlobClient CreateCloudBlobClient()
        {
            var storageAccount = CloudStorageAccount.Parse(_azureSettings.Value.StorageConnection);
            return storageAccount.CreateCloudBlobClient();
        }

        public async Task UploadBase64(string fileName, string content)
        {
            await _container.Value.CreateIfNotExistsAsync();
            var blockBlob = _container.Value.GetBlockBlobReference(fileName);

            // encode with base64
            var base64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(content));

            await blockBlob.UploadTextAsync(base64);
        }

        public async Task<string> DownloadBase64Async(string fileName)
        {
            await _container.Value.CreateIfNotExistsAsync();
            var blockBlob = _container.Value.GetBlockBlobReference(fileName);

            // decode with base64
            var base64 = await blockBlob.DownloadTextAsync();

            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }


        public async Task<string> DownloadTextAsync(string filename, string containerName)
        {
            _client.GetContainerReference(containerName);
            var blockBlob = _container.Value.GetBlockBlobReference(filename);

            return await blockBlob.DownloadTextAsync();
        }
    }
}
