using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;

namespace TheCarHub.Repositories
{
    public interface IListingRepository
    {
        void UpdateListing(ListingEntity listing);
        void SaveListing(ListingEntity listing);
        void DeleteListing(int id);
        Task<ListingEntity> GetListingById(int id);
        Task<IList<ListingEntity>> GetAllListings();
    }
}