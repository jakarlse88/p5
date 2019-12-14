using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TheCarHub.Models.Entities;

namespace TheCarHub.Services 
{
    public interface IMediaService
    {
        Task<Media> GetMediaByFileNameAsync(string fileName);
        bool RemoveMediaObject(Media media);
        Task<bool> RemoveAzureBlobMediaObjectAsync(Media media);
        void UpdateMediaCollection(IEnumerable<string> fileNames, Listing entity);
        Task<List<string>> UploadFiles(IList<IFormFile> files);
        Task<List<string>> UploadFileAzureBlobAsync(IList<IFormFile> files);
    }
}