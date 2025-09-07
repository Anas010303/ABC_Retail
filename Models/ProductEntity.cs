using Azure;
using Azure.Data.Tables;
namespace ABC_Retail.Models
{
    public class ProductEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "PRODUCT";
        public string RowKey { get; set; } // product id
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } // blob URL
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
