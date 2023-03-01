namespace AzureStorageOperations.Services
{
    public interface IFileShareStorageService
    {
        Task FileUploadAsync(IFormFile FileDetail);
        Task FileDownloadAsync(string fileShareName);
    }
}
