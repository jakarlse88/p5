using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;
using TheCarHub.Models.Entities;

namespace TheCarHub.Services
{
    public interface IListingService
    {
        Task<IEnumerable<Listing>> GetAllListings();
        Task<Listing> GetListingById(int id);
        void UpdateListing(Listing listing);
    }
}