using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void TrackListing(Listing listing)
        {
            if (listing != null)
            {
                _context.Add(listing);
            }
        }

        public async Task<IList<Listing>> GetAllListings()
        {
            var results = 
                await _context
                    .Listing
                    .Include(l => l.Car)
                    .Include(l => l.Status)                    
                    .Include(l => l.Media)
                    .Include(l => l.RepairJob)
                    .ToListAsync();

            return results;
        }

        public EntityEntry<Listing> GetListingEntityEntry(Listing entity)
        {
            if (entity == null) return null;
            
            var entry = _context.Entry(entity);

            return entry;
        }

        public async Task<Listing> GetListingById(int id)
        {
            var result =
                await _context
                    .Listing
                    .Include(l => l.Car)
                    .Include(l => l.Status)
                    .Include(l => l.Media)
                    .Include(l => l.RepairJob)
                    .SingleOrDefaultAsync(l => l.Id == id);

            return result;
        }

        public void AddListing(Listing listing)
        {
            if (listing == null) return;
            
            _context.Listing.Add(listing);
            _context.SaveChanges();
        }

        public void UpdateListing(Listing listing)
        {
            if (listing == null) return;
            
            _context.Listing.Update(listing);
            _context.SaveChanges();
        }
    }
}