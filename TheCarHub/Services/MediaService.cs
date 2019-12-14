using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using TheCarHub.Models;
using TheCarHub.Utilities;

namespace TheCarHub.Services
{
    public class MediaService : IMediaService
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IFileUtility _fileUtility;
        private readonly IFileSystem _fileSystem;

        public MediaService(IMediaRepository mediaRepository,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration, 
            IFileUtility fileUtility)
        {
            _mediaRepository = mediaRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _fileUtility = fileUtility;
            _fileSystem = new FileSystem();
        }
        
        public MediaService(IMediaRepository mediaRepository,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration, 
            IFileUtility fileUtility,
            IFileSystem fileSystem)
        {
            _mediaRepository = mediaRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _fileUtility = fileUtility;
            _fileSystem = fileSystem;
        }

        public async Task<Media> GetMediaByFileNameAsync(string fileName)
        {
            var media =
                await _mediaRepository.GetAllMedia();

            var result = media.FirstOrDefault(m => m.FileName == fileName);

            return result;
        }

        private void DeleteMedia(Media media)
        {
            UnregisterMediaFromListing(media);
            _mediaRepository.DeleteMedia(media);
        }

        public void UpdateMediaCollection(IEnumerable<string> fileNames, Listing entity)
        {
            if (fileNames == null || !fileNames.Any() || entity == null)
            {
                return;
            }

            var newMedia = MapImgNamesToMedia(fileNames).ToList();

            if (!newMedia.Any())
            {
                return;
            }

            foreach (var item in newMedia)
            {
                entity.Media.Add(item);
            }
        }

        private IEnumerable<Media> MapImgNamesToMedia(IEnumerable<string> imgNames)
        {
            var media = new HashSet<Media>();

            if (imgNames == null)
                return media;

            var enumerable = imgNames.ToList();

            if (enumerable.Any())
            {
                foreach (var name in enumerable)
                {
                    media.Add(new Media
                    {
                        FileName = name,
                        Tags = new List<MediaTag>()
                    });
                }
            }

            return media;
        }

        private void UnregisterMediaFromListing(Media media)
        {
            if (media == null) return;

            var listing = media.Listing;

            listing.Media.Remove(media);
        }

        public bool RemoveMediaObject(Media media)
        {
            if (media == null)
            {
                return false;
            }

            try
            {
                _fileUtility.DeleteImageFromDisk(
                    _webHostEnvironment,
                    _configuration,
                    media.FileName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            DeleteMedia(media);

            return true;
        }

        public async Task<List<string>> UploadFiles(IList<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return null;
            }

            var fileNames = new List<string>();

            foreach (var item in files)
            {
                var fileName =
                    await _fileUtility.UploadImageToDiskAsync(
                        _webHostEnvironment,
                        _configuration,
                        item);

                if (string.IsNullOrEmpty(fileName))
                    continue;

                fileNames.Add(fileName);
            }

            return fileNames;
        }
        
        public async Task<bool> RemoveAzureBlobMediaObjectAsync(Media media)
        {
            var container = GetAzureCloudBlobContainerReference(_configuration["AZURE_MEDIA_CONTAINER_NAME"]);
            
            var blockBlob = container.GetBlockBlobReference(media.FileName);

            try
            {
                await blockBlob.DeleteAsync();                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                DeleteMedia(media);
            }

            return true;
        }

        public async Task<List<string>> UploadFileAzureBlobAsync(IList<IFormFile> files)
        {
            var container = GetAzureCloudBlobContainerReference(_configuration["AZURE_MEDIA_CONTAINER_NAME"]);

            var fileNames = new List<string>();

            foreach (var file in files)
            {
                var fileName =
                    _fileSystem.Path.GetRandomFileName() +
                    _fileSystem.Path.GetExtension(file.FileName);

                var blockBlob = container.GetBlockBlobReference(fileName);

                await using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    
                    stream.Seek(0, SeekOrigin.Begin);

                    await blockBlob.UploadFromStreamAsync(stream);

                    stream.Close();
                }

                fileNames.Add(fileName);
            }

            return fileNames;
        }

        private CloudBlobContainer GetAzureCloudBlobContainerReference(string containerName)
        {
            var credentials = new StorageCredentials(
                "csb05a6c77618cex4803xb98",
                "xlhGlMJLGMRcGgb/QpgMmPJKzdF2MGR8aZCxNkyR2SKvuwooIg/1xTKnp7UeNIGrFsfg0kC+ZLl5gZZKKkgyMA==");

            var account = new CloudStorageAccount(credentials, true);

            var client = account.CreateCloudBlobClient();

            var container = client.GetContainerReference(containerName);
            
            return container;
        }
    }
}