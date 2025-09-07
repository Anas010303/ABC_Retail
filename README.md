# ABC Retail Web Application – Azure Storage Demo

## Project Overview

This is a web application for ABC Retail that demonstrates the use of **Azure Storage Services**:

* **Azure Tables** – store customer and product information
* **Azure Blob Storage** – store product images
* **Azure Queue Storage** – store order processing messages

The app helps manage data efficiently and is ready to deploy on Azure.
## Prerequisites

* Visual Studio
* .NET 8
* Azure Storage Account with: Tables, Blobs, and Queue
* Internet connection for Azure deployment
* 
## Setup Instructions

1. Clone the repository:

```bash
git clone <YOUR_GITHUB_REPO_LINK>
```

2. Update `appsettings.json` with your Azure Storage info:

```json
"Storage": {
  "ConnectionString": "<YOUR_CONNECTION_STRING>",
  "BlobContainer": "productimages",
  "CustomerTable": "Customers",
  "ProductTable": "Products",
  "QueueName": "processing-queue"
}
```

3. Install NuGet packages:

```powershell
Install-Package Azure.Data.Tables
Install-Package Azure.Storage.Blobs
Install-Package Azure.Storage.Queues
```

## Running the Application

* Run locally using Visual Studio
* Navigate to: `https://localhost:7100/Storage`

### Populate Dummy Data

Visit these URLs once to insert sample data:

* `/Storage/PopulateTables` – Customers & Products
* `/Storage/PopulateBlobs` – Images
* `/Storage/PopulateQueues` – Queue messages

Then check:

* `/Storage/Tables`
* `/Storage/Blobs`
* `/Storage/Queues`

## Accessing the Application

* **Local:** `https://localhost:7100/Storage`
* **Deployed:** `http://student<number>.azurewebsites.net/`
* **GitHub Source Code:** `<YOUR_GITHUB_REPO_LINK>`

## Project Structure

```
Controllers/
    StorageController.cs
Services/
    TableStorageService.cs
    BlobStorageService.cs
    QueueService.cs
Views/Storage/
    Index.cshtml
    Tables.cshtml
    Blobs.cshtml
    Queues.cshtml
appsettings.json
```

## References

* [Azure Tables](https://learn.microsoft.com/en-us/azure/storage/tables/table-storage-overview)
* [Azure Blobs](https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction)
* [Azure Queues](https://learn.microsoft.com/en-us/azure/storage/queues/storage-queues-introduction)
