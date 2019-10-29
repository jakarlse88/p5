using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;

namespace TheCarHub.Repositories
{
    public interface IListingRepository
    {
        void UpdateListing(Listing listing);
        void SaveListing(Listing listing);
        void DeleteListing(int id);
        Task<Listing> GetListingById(int id);
        Task<IList<Listing>> GetAllListings();
    }
}