using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Shared.Models.DomainModels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Table;

namespace Shared.Infrastructure.Azure
{
    public class AzureTableRepo : IStartUp, ITableRepo
    {

        IConfiguration _configuration;
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable tableCloud;

        public AzureTableRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool AddEntity(object payload, string table, string partitionKey, string rowKey)
        {
            Dictionary<string, EntityProperty> flattenedProperties = EntityPropertyConverter.Flatten(payload, null);

            DynamicTableEntity entity = new DynamicTableEntity();
            entity.PartitionKey = partitionKey;
            entity.RowKey = rowKey;
            entity.Properties = flattenedProperties;

            storageAccount = CloudStorageAccount.Parse(this._configuration.GetConnectionString("Storage"));
            tableClient = storageAccount.CreateCloudTableClient();
            tableCloud = tableClient.GetTableReference(table);

            TableOperation operation = TableOperation.InsertOrReplace(entity);           
            
            try
            {
                tableCloud.ExecuteAsync(operation).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown @ {DateTime.Now} : Where: {ex.StackTrace}");
                return false;
            }          

            return true;

        }

     
        public void DeleteEntity(string payload, string table)
        {
            throw new NotImplementedException();
        }

        public void OnInIt()
        {
            var azureTables = _configuration.GetSection("Tables").GetChildren();

            storageAccount = CloudStorageAccount.Parse(this._configuration.GetConnectionString("Storage"));
            tableClient = storageAccount.CreateCloudTableClient();

            // Create the Table if it doesn't already exist. 
            foreach (var azureTable in azureTables)
            {
                tableCloud = tableClient.GetTableReference(azureTable.Value.ToString());
                tableCloud.CreateIfNotExistsAsync();
            }
        }
    }
}
