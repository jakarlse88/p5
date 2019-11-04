using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;

namespace TheCarHub.Repositories
{
    public interface IListingRepository
    {
        void Edit(Listing listing);
        void Add(Listing listing);
        void Delete(int id);
        Task<Listing> GetById(int id);
        Task<IList<Listing>> GetAll();
    }
}