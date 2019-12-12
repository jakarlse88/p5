using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public interface IListingRepository
    {
        void UpdateListing(Listing listing);
        void AddListing(Listing listing);
        void TrackListing(Listing listing);
        Task<Listing> GetListingById(int id);
        Task<IList<Listing>> GetAllListings();
        EntityEntry<Listing> GetListingEntityEntry(Listing entity);
    }
}