using AzureStorageOperations.Models;

namespace AzureStorageOperations.Services
{
    public interface ITableStorageService
    {
        Task<GroceryItemEntity> GetEntityAsync(string category, string id);
        Task<GroceryItemEntity> UpsertEntityAsync(GroceryItemEntity entity);
        Task DeleteEntityAsync(string category, string id);
    }
}
