using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;
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
            // TODO: test
            var listing = await _listingRepository.GetListingById(id);

            // var model = new ListingViewModel
            // {
            //     Id = listing.Id,
            //     Title = listing.Title,
            //     Car = listing.Car,
            //     Media = listing.Media,
            //     Description = listing.Description,
            //     ListingTags = listing.ListingTags,
            //     Status = listing.Status,
            //     DateCreated = listing.DateCreated,
            //     DateLastUpdated = listing.DateLastUpdated,
            //     PurchaseDate = listing.PurchaseDate,
            //     PurchasePrice = listing.PurchasePrice,
            //     RepairJob = listing.RepairJob,
            //     SellingPrice = listing.SellingPrice,
            //     SaleDate = listing.SaleDate
            // };

            return listing;
        }

        public void UpdateListing(Listing listing)
        {
            _listingRepository.UpdateListing(listing);
        }
    }
}