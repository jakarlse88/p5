using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Media> GetMediaById(int id)
        {
            var result = 
                await _context
                .Media
                .Include(m => m.Listing)
                .FirstOrDefaultAsync(m => m.Id == id);

            return result;
        }

        public void EditMedia(Media media)
        {
            if (media != null)
            {
                _context.Media.Update(media);
                _context.SaveChanges();
            }
        }

        public void DeleteMedia(Media media)
        {
            if (media != null)
            {
                _context.Remove(media);
                _context.SaveChanges();
            }
        }
    }
}