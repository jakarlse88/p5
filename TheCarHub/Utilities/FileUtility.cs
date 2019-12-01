using System;
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
        public static async Task<string> UploadImageToDiskAsync(
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            IFormFile file)
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

            await using (var stream = File.Create(path))
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

        public static void DeleteImageFromDisk(
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            string fileName)
        {
            var path =
                Path.Combine(webHostEnvironment.WebRootPath,
                    configuration["Media:Directory"], $"{fileName}");

            if (System.IO.File.Exists(path))
            {
                try
                {
                    System.IO.File.Delete(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }       
            }
        }
    }
}