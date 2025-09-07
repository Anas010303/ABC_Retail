using Azure;
using Azure.Data.Tables;
namespace ABC_Retail.Models
{
    public class CustomerEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "CUSTOMER"; // simple partitioning
        public string RowKey { get; set; } // unique id, e.g. Guid
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
