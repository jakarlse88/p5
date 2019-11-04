using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;
using TheCarHub.Repositories;

namespace TheCarHub.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;

        public ListingService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<IEnumerable<Listing>> GetAll()
        {
            var results = await _listingRepository.GetAll();

            return results;
        }

        public async Task<Listing> GetById(int id)
        {
            var listing = await _listingRepository.GetById(id);

            return listing;
        }

        public void Edit(Listing listing)
        {
            if (listing != null)
            {
                _listingRepository.Edit(listing);
            }
        }

        public void Add(Listing listing)
        {
            if (listing != null)
            {
                _listingRepository.Add(listing);
            }
        }

        public void Delete(int id)
        {
            _listingRepository.Delete(id);
        }
    }
}