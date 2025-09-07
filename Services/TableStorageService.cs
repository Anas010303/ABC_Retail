using Azure.Data.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ABC_Retail.Services
{
    public class TableStorageService
    {
        private readonly TableClient _customerTable;
        private readonly TableClient _productTable;

        public TableStorageService(IConfiguration config)
        {
            var conn = config["Storage:ConnectionString"];
            _customerTable = new TableClient(conn, config["Storage:CustomerTable"]);
            _customerTable.CreateIfNotExists();

            _productTable = new TableClient(conn, config["Storage:ProductTable"]);
            _productTable.CreateIfNotExists();
        }

        // Insert a customer
        public async Task InsertCustomerAsync(string partitionKey, string rowKey, string name, string email)
        {
            var entity = new TableEntity(partitionKey, rowKey)
            {
                { "Name", name },
                { "Email", email }
            };
            await _customerTable.AddEntityAsync(entity);
        }

        // Insert a product
        public async Task InsertProductAsync(string partitionKey, string rowKey, string productName, double price)
        {
            var entity = new TableEntity(partitionKey, rowKey)
            {
                { "ProductName", productName },
                { "Price", price }
            };
            await _productTable.AddEntityAsync(entity);
        }

        // List customers
        public async Task<List<TableEntity>> ListCustomersAsync()
        {
            var result = new List<TableEntity>();
            await foreach (var entity in _customerTable.QueryAsync<TableEntity>())
            {
                result.Add(entity);
            }
            return result;
        }

        // List products
        public async Task<List<TableEntity>> ListProductsAsync()
        {
            var result = new List<TableEntity>();
            await foreach (var entity in _productTable.QueryAsync<TableEntity>())
            {
                result.Add(entity);
            }
            return result;
        }

        // Dummy data for testing
        public async Task InsertDummyDataAsync()
        {
            // Customers
            await InsertCustomerAsync("CUST", "001", "John Doe", "john@example.com");
            await InsertCustomerAsync("CUST", "002", "Jane Smith", "jane@example.com");
            await InsertCustomerAsync("CUST", "003", "Alice Brown", "alice@example.com");
            await InsertCustomerAsync("CUST", "004", "Bob Green", "bob@example.com");
            await InsertCustomerAsync("CUST", "005", "Charlie White", "charlie@example.com");

            // Products
            await InsertProductAsync("PROD", "001", "T-Shirt", 199.99);
            await InsertProductAsync("PROD", "002", "Jeans", 499.50);
            await InsertProductAsync("PROD", "003", "Jacket", 899.00);
            await InsertProductAsync("PROD", "004", "Sneakers", 699.99);
            await InsertProductAsync("PROD", "005", "Hat", 149.99);
        }
    }
}
