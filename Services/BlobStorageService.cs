using Azure.Storage.Blobs;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ABC_Retail.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient _container;

        public BlobStorageService(IConfiguration config)
        {
            var conn = config["Storage:ConnectionString"];
            var containerName = config["Storage:BlobContainer"];
            _container = new BlobContainerClient(conn, containerName);
            _container.CreateIfNotExists();
        }

        public async Task UploadBlobAsync(string fileName, byte[] content)
        {
            var blobClient = _container.GetBlobClient(fileName);
            using (var stream = new MemoryStream(content))
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }
        }

        public async Task<List<string>> ListBlobsAsync()
        {
            var blobs = new List<string>();
            await foreach (var blob in _container.GetBlobsAsync())
            {
                blobs.Add(blob.Name);
            }
            return blobs;
        }

        // Dummy blobs for testing
        public async Task UploadDummyBlobsAsync()
        {
            byte[] sampleData = System.Text.Encoding.UTF8.GetBytes("This is a sample blob");
            await UploadBlobAsync("image1.png", sampleData);
            await UploadBlobAsync("image2.png", sampleData);
            await UploadBlobAsync("image3.png", sampleData);
            await UploadBlobAsync("image4.png", sampleData);
            await UploadBlobAsync("image5.png", sampleData);
        }
    }
}
