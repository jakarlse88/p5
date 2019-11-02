using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCarHub.Data;
using TheCarHub.Models;
using Microsoft.EntityFrameworkCore;

namespace TheCarHub.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteListing(int id)
        {
            Listing listing =
                _context
                .Listing
                .FirstOrDefault(l => l.Id == id);
            
            if (listing != null)
            {
                _context.Listing.Remove(listing);
                _context.SaveChanges();
            }
        }

        public async Task<IList<Listing>> GetAllListings()
        {
            var results = await _context.Listing.ToListAsync();

            return results;
        }

        public async Task<Listing> GetListingById(int id)
        {
            // Listing result =
            //     await _context
            //         .Listing
            //         .FirstOrDefaultAsync(l => l.Id == id);

            Listing result =
                await _context
                .Listing
                .Include(l => l.Media)
                .SingleOrDefaultAsync(l => l.Id == id);

            return result;
        }

        public void SaveListing(Listing listing)
        {
            if (listing != null)
            {
                _context.Listing.Add(listing);
                _context.SaveChanges();
            }
        }

        public void UpdateListing(Listing listing)
        {
            if (listing != null)
            {
                _context.Listing.Update(listing);
                _context.SaveChanges();
            }
        }
    }
}