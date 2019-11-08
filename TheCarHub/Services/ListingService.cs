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

        public async Task<IEnumerable<Listing>> GetAllListings()
        {
            var results = await _listingRepository.GetAllListings();

            return results;
        }

        public async Task<Listing> GetListingById(int id)
        {
            var listing = await _listingRepository.GetListingById(id);

            return listing;
        }

        public void EditListing(Listing listing)
        {
            if (listing != null)
            {
                _listingRepository.EditListing(listing);
            }
        }

        public void AddListing(Listing listing)
        {
            if (listing != null)
            {
                _listingRepository.AddListing(listing);
            }
        }

        public void DeleteListing(int id)
        {
            _listingRepository.DeleteListing(id);
        }
    }
}