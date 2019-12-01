using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using System.Linq;

namespace TheCarHub.Services
{
    public class MediaService : IMediaService
    {
        private readonly IMediaRepository _mediaRepository;

        public MediaService(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public void AddMedia(Media media)
        {
            if (media != null)
            {
                _mediaRepository.AddMedia(media);
            }
        }

        public async Task<IEnumerable<Media>> GetAllMedia()
        {
            var results = await _mediaRepository.GetAllMedia();

            return results;
        }

        public async Task<Media> GetMediaByIdAsync(int id)
        {
            var result = await _mediaRepository.GetMediaById(id);

            return result;
        }

        public async Task<Media> GetMediaByFileNameAsync(string fileName)
        {
            var media = 
                await _mediaRepository.GetAllMedia();

            var result = media.FirstOrDefault(m => m.FileName == fileName);

            return result;
        }

        public void EditMedia(Media media)
        {
            if (media != null)
            {
                _mediaRepository.EditMedia(media);
            }
        }

        public void DeleteMedia(Media media)
        {
            UnregisterMediaFromListing(media);
            _mediaRepository.DeleteMedia(media);
        }

        private void UnregisterMediaFromListing(Media media)
        {
            if (media == null) return;

            var listing = media.Listing;

            listing.Media.Remove(media);
        }
    }
}