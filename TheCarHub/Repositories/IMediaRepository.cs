using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public interface IMediaRepository
    {
        void AddMedia(Media media);
        Task<IEnumerable<Media>> GetAllMedia();
        Task<Media> GetMediaById(int id);
        void UpdateMedia(Media media);
        void DeleteMedia(Media media);
        EntityEntry<Media> GetMediaEntityEntry(Media media);
    }
}