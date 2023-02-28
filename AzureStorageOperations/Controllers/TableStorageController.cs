using AzureStorageOperations.Models;
using AzureStorageOperations.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageOperations.Controllers
{
    [Route("api/[controller]")]
    public class TableStorageController : Controller
    {
        private readonly ITableStorageService _storageService;
        public TableStorageController(ITableStorageService storageService)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        }

        [HttpGet]
        [Route("GetTableData")]
        public async Task<IActionResult> GetAsync([FromQuery] string category, string id)
        {
            return Ok(await _storageService.GetEntityAsync(category, id));
        }

        [HttpPost]
        [Route("InsertTableData")]
        public async Task<IActionResult> PostAsync(GroceryItemEntity entity)
        {
            entity.PartitionKey = entity.Category;
            string Id = Guid.NewGuid().ToString();
            entity.Id = Id;
            entity.RowKey = Id;
            var createdEntity = await _storageService.UpsertEntityAsync(entity);
            return CreatedAtAction(nameof(GetAsync), createdEntity);
        }

        [HttpPut]
        [Route("UpdateTableData")]
        public async Task<IActionResult> PutAsync([FromBody] GroceryItemEntity entity)
        {
            entity.PartitionKey = entity.Category;
            entity.RowKey = entity.Id;
            await _storageService.UpsertEntityAsync(entity);
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteTableData")]
        public async Task<IActionResult> DeleteAsync([FromQuery] string category, string id)
        {
            await _storageService.DeleteEntityAsync(category, id);
            return NoContent();
        }
    }
}
