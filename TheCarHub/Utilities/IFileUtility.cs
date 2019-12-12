using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TheCarHub.Utilities
{
    public interface IFileUtility
    {
        Task<string> UploadImageToDiskAsync(
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            IFormFile file);

        void DeleteImageFromDisk(
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            string fileName);
    }
}