using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;

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

        public async Task<Media> GetMediaById(int id)
        {
            var result = await _mediaRepository.GetMediaById(id);

            return result;
        }

        public void EditMedia(Media media)
        {
            if (media != null)
            {
                _mediaRepository.EditMedia(media);
            }
        }

        public void DeleteMedia(int id)
        {
            _mediaRepository.DeleteMedia(id);
        }
    }
}