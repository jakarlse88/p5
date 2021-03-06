using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCarHub.Data;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly ApplicationDbContext _context;

        public MediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddMedia(Media media)
        {
            if (media != null)
            {
                _context.Media.Add(media);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Media>> GetAllMedia()
        {
            var results = 
                await _context
                    .Media
                    .Include(m => m.Listing)
                    .ToListAsync();

            return results;
        }

        public void DeleteMedia(Media media)
        {
            if (media != null)
            {
                _context.Remove(media);
                _context.SaveChanges();
            }
        }

        public EntityEntry<Media> GetMediaEntityEntry(Media media)
        {
            if (media == null) return null;
            
            var entry = _context.Entry(media);

            return entry;
        }
    }
}