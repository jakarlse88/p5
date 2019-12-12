using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;
using TheCarHub.Models.ViewModels;

namespace TheCarHub.Services
{
    public interface IListingService
    {
        Task<IEnumerable<Listing>> GetAllListings();
        Task<IEnumerable<ListingViewModel>> GetAllListingsAsViewModel();
        Task<IEnumerable<ListingViewModel>> GetFilteredListingViewModels(int status, string query);
        Task<Listing> GetListingByIdAsync(int id);
        Task<ListingInputModel> GetListingInputModelByIdAsync(int id);
        Task<ListingViewModel> GetListingViewModelByIdAsync(int id);
        Task AddListingAsync(ListingInputModel inputModel);
        Task<bool> UpdateListingAsync(ListingInputModel source);
    }
}