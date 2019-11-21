using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TheCarHub.Utilities
{
    public static class FileUtility 
    {
        public static async Task<string> UploadImage(IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,IFormFile file)
        {
            var validExtensions = new List<string> {".jpg", ".jpeg", ".png"};

            var extension = Path.GetExtension(file.FileName);

            if (!validExtensions.Contains(extension.ToLower()))
                return null;
            
            var fileName =
                Path.GetRandomFileName() + Path.GetExtension(file.FileName);

            var path =
                Path.Combine(webHostEnvironment.WebRootPath,
                    configuration["Media:Directory"], $"{fileName}");

            await using (var stream = System.IO.File.Create(path))
            {
                try
                {
                    await file.CopyToAsync(stream);
                }
                catch
                {
                    return null;
                }
            }

            return fileName;
        }
    }
}