using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;

namespace TheCarHub.Services 
{
    public interface IMediaService
    {
        void AddMedia(Media media);
        Task<IEnumerable<Media>> GetAllMedia();
        Task<Media> GetMediaByIdAsync(int id);
        Task<Media> GetMediaByFileNameAsync(string fileName);
        void EditMedia(Media media);
        void DeleteMedia(Media media);
        void UpdateMediaExperimental(IEnumerable<string> fileNames, Listing entity);
    }
}