using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;

namespace TheCarHub.Repositories
{
    public interface IListingRepository
    {
        void Save(ListingEntity listing);
        Task<ListingEntity> GetListingById(int id);
        Task<IList<ListingEntity>> GetAllListings();
    }
}