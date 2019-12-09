using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using TheCarHub.Models;

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
                _mediaRepository.UpdateMedia(media);
            }
        }

        public void DeleteMedia(Media media)
        {
            UnregisterMediaFromListing(media);
            _mediaRepository.DeleteMedia(media);
        }

        public void UpdateMediaExperimental(IEnumerable<string> fileNames, Listing entity)
        {
            if (fileNames == null || !fileNames.Any() || entity == null)
            {
                return;
            }
            
            var newMedia = MapImgNamesToMedia(fileNames);

            if (!newMedia.Any())
            {
                return;
            }
            
            foreach (var item in newMedia)
            {
//                _mediaRepository.AddMedia(item);
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
    }
}