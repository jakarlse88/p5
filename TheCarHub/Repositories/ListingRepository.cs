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
                .Listings
                .FirstOrDefault(l => l.Id == id);
            
            if (listing != null)
            {
                _context.Listings.Remove(listing);
                _context.SaveChanges();
            }
        }

        public async Task<IList<Listing>> GetAllListings()
        {
            var results = await _context.Listings.ToListAsync();

            return results;
        }

        public async Task<Listing> GetListingById(int id)
        {
            Listing result =
                await _context
                    .Listings
                    .FirstOrDefaultAsync(l => l.Id == id);

            return result;
        }

        public void SaveListing(Listing listing)
        {
            if (listing != null)
            {
                _context.Listings.Add(listing);
                _context.SaveChanges();
            }
        }

        public void UpdateListing(Listing listing)
        {
            if (listing != null)
            {
                _context.Listings.Update(listing);
                _context.SaveChanges();
            }
        }
    }
}