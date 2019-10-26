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
            ListingEntity listing =
                _context
                .Listings
                .FirstOrDefault(l => l.ListingId == id);
            
            if (listing != null)
            {
                _context.Listings.Remove(listing);
                _context.SaveChanges();
            }
        }

        public async Task<IList<ListingEntity>> GetAllListings()
        {
            var results = await _context.Listings.ToListAsync();

            return results;
        }

        public async Task<ListingEntity> GetListingById(int id)
        {
            ListingEntity result =
                await _context
                    .Listings
                    .FirstOrDefaultAsync(l => l.ListingId == id);

            return result;
        }

        public void SaveListing(ListingEntity listing)
        {
            if (listing != null)
            {
                _context.Listings.Add(listing);
                _context.SaveChanges();
            }
        }

        public void UpdateListing(ListingEntity listing)
        {
            if (listing != null)
            {
                _context.Listings.Update(listing);
                _context.SaveChanges();
            }
        }
    }
}