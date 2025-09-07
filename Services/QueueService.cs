using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ABC_Retail.Services
{
    public class QueueService
    {
        private readonly QueueClient _queue;

        public QueueService(IConfiguration config)
        {
            var conn = config["Storage:ConnectionString"];
            var queueName = config["Storage:QueueName"];
            _queue = new QueueClient(conn, queueName);
            _queue.CreateIfNotExists();
        }

        public async Task EnqueueMessageAsync(string message)
        {
            await _queue.SendMessageAsync(message);
        }

        public async Task<List<QueueMessage>> ListMessagesAsync(int max = 10)
        {
            var res = await _queue.ReceiveMessagesAsync(max);
            return res.Value.ToList();
        }

        // Dummy messages for testing
        public async Task EnqueueDummyMessagesAsync()
        {
            await EnqueueMessageAsync("Process order #1001");
            await EnqueueMessageAsync("Process order #1002");
            await EnqueueMessageAsync("Process order #1003");
            await EnqueueMessageAsync("Upload image image1.png");
            await EnqueueMessageAsync("Send invoice #5001");
        }
    }
}
