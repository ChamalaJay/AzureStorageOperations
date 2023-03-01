using AzureStorageOperations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureStorageOperations.Controllers
{
    public class FileShareStorageController : Controller
    {
        private readonly IFileShareStorageService fileShareStorageService;

        public FileShareStorageController(IFileShareStorageService _fileShareStorageService)
        {
            this.fileShareStorageService = _fileShareStorageService;
        }

        /// <summary>
        /// upload file
        /// </summary>
        /// <param name="fileDetail"></param>
        /// <returns></returns>
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile fileDetail)
        {
            if (fileDetail != null)
            {
                await fileShareStorageService.FileUploadAsync(fileDetail);
            }
            return Ok();
        }

        /// <summary>
        /// download file
        /// </summary>
        /// <param name="fileDetail"></param>
        /// <returns></returns>
        [HttpPost("Download")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            if (fileName != null)
            {
                await fileShareStorageService.FileDownloadAsync(fileName);
            }
            return Ok();
        }
    }
}
