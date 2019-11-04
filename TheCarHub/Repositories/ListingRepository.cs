using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCarHub.Data;
using TheCarHub.Models;
using Microsoft.EntityFrameworkCore;
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

        public void Delete(int id)
        {
            var listing =
                _context
                .Listing
                .FirstOrDefault(l => l.Id == id);
            
            if (listing != null)
            {
                _context.Listing.Remove(listing);
                _context.SaveChanges();
            }
        }

        public async Task<IList<Listing>> GetAll()
        {
            var results = await _context.Listing.ToListAsync();

            return results;
        }

        public async Task<Listing> GetById(int id)
        {
            var result =
                await _context
                .Listing
                .Include(l => l.Media)
                .SingleOrDefaultAsync(l => l.Id == id);

            return result;
        }

        public void Add(Listing listing)
        {
            if (listing != null)
            {
                _context.Listing.Add(listing);
                _context.SaveChanges();
            }
        }

        public void Edit(Listing listing)
        {
            if (listing != null)
            {
                _context.Listing.Update(listing);
                _context.SaveChanges();
            }
        }
    }
}