using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public interface IListingRepository
    {
        void EditListing(Listing listing);
        void AddListing(Listing listing);
        void DeleteListing(int id);
        Task<Listing> GetListingById(int id);
        Task<IList<Listing>> GetAllListings();
    }
}