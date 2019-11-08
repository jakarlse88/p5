using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;

namespace TheCarHub.Services 
{
    public interface IMediaService
    {
        void AddMedia(Media media);
        Task<IEnumerable<Media>> GetAllMedia();
        Task<Media> GetMediaById(int id);
        void EditMedia(Media media);
        void DeleteMedia(int id);
    }
}