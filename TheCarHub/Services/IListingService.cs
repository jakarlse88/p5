using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;

namespace TheCarHub.Services
{
    public interface IListingService
    {
        Task<IEnumerable<Listing>> GetAllListings();
        Task<Listing> GetListingById(int id);
        void EditListing(Listing listing);
        Task AddListing(ListingInputModel inputModel);
        void DeleteListing(int id);
        void ValidateListingInputModel(ModelStateDictionary modelState, ListingInputModel inputModel);
    }
}