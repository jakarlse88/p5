using TheCarHub.Data;
using TheCarHub.Models;
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

        public void Add(Media media)
        {
            if (media != null)
            {
                _context.Media.Add(media);
                _context.SaveChanges();
            }
        }
    }
}