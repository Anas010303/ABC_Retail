using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
namespace ABC_Retail.Services
{
    public class FileShareService
    {
        private readonly ShareClient _shareClient;

        public FileShareService(IConfiguration config)
        {
            var conn = config["Storage:ConnectionString"];
            var shareName = config["Storage:FileShare"];
            _shareClient = new ShareClient(conn, shareName);
            _shareClient.CreateIfNotExists();
        }

        public async Task UploadFileAsync(string directory, string filename, Stream data)
        {
            var dirClient = _shareClient.GetDirectoryClient(directory);
            await dirClient.CreateIfNotExistsAsync();
            var fileClient = dirClient.GetFileClient(filename);
            await fileClient.CreateAsync(data.Length);
            data.Position = 0;
            await fileClient.UploadRangeAsync(new HttpRange(0, data.Length), data);
        }

        public async Task<IEnumerable<ShareFileItem>> ListFilesAsync(string directory)
        {
            var dirClient = _shareClient.GetDirectoryClient(directory);
            var list = new List<ShareFileItem>();
            await foreach (var item in dirClient.GetFilesAndDirectoriesAsync())
                list.Add(item);
            return list;
        }
    }
}
