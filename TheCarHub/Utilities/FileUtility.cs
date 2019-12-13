using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TheCarHub.Utilities
{
    public class FileUtility : IFileUtility
    {
        private readonly IFileSystem _fileSystem;

        public FileUtility()
        {
            _fileSystem = new FileSystem();
        }

        public FileUtility(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public async Task<string> UploadImageToDiskAsync(
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            IFormFile file)
        {
            if (webHostEnvironment == null ||
                configuration == null ||
                file == null)
            {
                return null;
            }

            var validExtensions = new List<string> {".jpg", ".jpeg", ".png"};

            var extension = _fileSystem.Path.GetExtension(file.FileName);

            if (!validExtensions.Contains(extension.ToLower()))
            {
                return null;
            }

            var fileName =
                _fileSystem.Path.GetRandomFileName() +
                _fileSystem.Path.GetExtension(file.FileName);

            var path =
                _fileSystem.Path.Combine(webHostEnvironment.ContentRootPath,
                    configuration["Media:Directory"], $"{fileName}");

            await using (var stream = _fileSystem.File.Create(path))
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

        public void DeleteImageFromDisk(
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            string fileName)
        {
            if (webHostEnvironment == null ||
                configuration == null ||
                string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            var path =
                _fileSystem.Path.Combine(webHostEnvironment.WebRootPath,
                    configuration["Media:Directory"], $"{fileName}");

            if (_fileSystem.File.Exists(path))
            {
                try
                {
                    _fileSystem.File.Delete(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}