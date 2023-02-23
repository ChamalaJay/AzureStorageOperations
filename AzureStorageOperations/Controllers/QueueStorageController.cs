using Azure.Storage.Queues.Models;
using AzureStorageOperations.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageOperations.Controllers
{
    public class QueueStorageController
    {
        private readonly IQueueStorageService _storageService;
        private readonly string _connectionString;
        private readonly string _container;

        public QueueStorageController(IQueueStorageService storageService, IConfiguration iConfig)
        {
            _storageService = storageService;
            _connectionString = iConfig.GetValue<string>("MyConfig:StorageConnection");
            _container = iConfig.GetValue<string>("MyConfig:ContainerName");
        }

        [HttpGet("GetMessageFromQueue")]
        public PeekedMessage ListFiles(string queueName)
        {
            return _storageService.PeekMessage(_connectionString, queueName);
        }

        [Route("CreateQueue")]
        [HttpPost]
        public bool CreateQueue(string queueName)
        {
            if (!string.IsNullOrEmpty(queueName))
            {
                _storageService.CreateQueue(_connectionString, queueName);
                return true;
            }

            return false;
        }

        [HttpGet("SendMessageToQueue")]
        public bool SendMessageToQueue(string queueName, string message)
        {
            var content =  _storageService.SendMessageToQueue(_connectionString, queueName, message);
            return content;
        }


        [HttpGet("SendMessageToQueue")]
        public bool UpdateMessage(string queueName, string newMessage)
        {
            var result = _storageService.UpdateMessage(_connectionString, queueName, newMessage);
            return result;
        }

        [Route("DeleteQueue")]
        [HttpGet]
        public bool DeleteQueue(string queueName)
        {
            return _storageService.DeleteQueue(_connectionString, queueName);
        }
    }
}
