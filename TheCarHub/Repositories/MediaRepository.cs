using System.Collections.Generic;
using System.Linq;
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
            var results = await _context.Media.ToListAsync();

            return results;
        }

        public async Task<Media> GetMediaById(int id)
        {
            var result = 
                await _context
                .Media
                .FirstOrDefaultAsync(m => m.Id == id);

            return result;
        }

        public async void EditMedia(Media media)
        {
            if (media != null)
            {
                _context.Media.Update(media);
                _context.SaveChanges();
            }
        }

        public void DeleteMedia(int id)
        {
            var media =
                _context
                    .Media
                    .FirstOrDefault(m => m.Id == id);

            if (media != null)
            {
                _context.Remove(media);
                _context.SaveChanges();
            }
        }
    }
}