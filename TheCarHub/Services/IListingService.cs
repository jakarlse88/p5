using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;
using TheCarHub.Models.Entities;

namespace TheCarHub.Services
{
    public interface IListingService
    {
        Task<IEnumerable<Listing>> GetAll();
        Task<Listing> GetById(int id);
        void Edit(Listing listing);
        void Add(Listing listing);
        void Delete(int id);
    }
}