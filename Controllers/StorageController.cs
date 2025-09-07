using Microsoft.AspNetCore.Mvc;
using ABC_Retail.Services;
using System.Threading.Tasks;

namespace ABC_Retail.Controllers
{
    public class StorageController : Controller
    {
        private readonly TableStorageService _tableService;
        private readonly BlobStorageService _blobService;
        private readonly QueueService _queueService;

        public StorageController(
            TableStorageService tableService,
            BlobStorageService blobService,
            QueueService queueService)
        {
            _tableService = tableService;
            _blobService = blobService;
            _queueService = queueService;
        }

        // Landing page for storage
        public IActionResult Index()
        {
            return View();
        }

        // Azure Tables
        public async Task<IActionResult> Tables()
        {
            var customers = await _tableService.ListCustomersAsync();
            var products = await _tableService.ListProductsAsync();
            ViewBag.Customers = customers;
            ViewBag.Products = products;
            return View();
        }

        // Azure Blobs
        public async Task<IActionResult> Blobs()
        {
            var blobs = await _blobService.ListBlobsAsync();
            ViewBag.Blobs = blobs;
            return View();
        }

        // Azure Queues
        public async Task<IActionResult> Queues()
        {
            var messages = await _queueService.ListMessagesAsync();
            ViewBag.Messages = messages;
            return View();
        }

        // Temporary actions to populate dummy data

        public async Task<IActionResult> PopulateTables()
        {
            await _tableService.InsertDummyDataAsync();
            return Content("Dummy tables data inserted!");
        }

        public async Task<IActionResult> PopulateBlobs()
        {
            await _blobService.UploadDummyBlobsAsync();
            return Content("Dummy blobs uploaded!");
        }

        public async Task<IActionResult> PopulateQueues()
        {
            await _queueService.EnqueueDummyMessagesAsync();
            return Content("Dummy queue messages inserted!");
        }
    }
}
