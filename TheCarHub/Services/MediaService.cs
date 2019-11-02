using TheCarHub.Models;
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

        public void Add(Media media)
        {
            if (media != null)
            {
                _mediaRepository.Add(media);
            }
        }
    }
}