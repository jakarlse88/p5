using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;
using TheCarHub.Models.ViewModels;
using TheCarHub.Repositories;

namespace TheCarHub.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IRepairJobService _repairJobService;
        private readonly ICarService _carService;
        private readonly IStatusService _statusService;
        private readonly IMediaService _mediaService;
        private readonly IMapper _mapper;

        public ListingService(IListingRepository listingRepository,
            IRepairJobService repairJobService,
            ICarService carService,
            IStatusService statusService,
            IMediaService mediaService,
            IMapper mapper)
        {
            _listingRepository = listingRepository;
            _repairJobService = repairJobService;
            _carService = carService;
            _statusService = statusService;
            _mediaService = mediaService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Listing>> GetAllListings()
        {
            var results = await _listingRepository.GetAllListings();

            return results;
        }

        public async Task<IEnumerable<ListingViewModel>> GetAllListingsAsViewModel()
        {
            var listings = await GetAllListings();

            var viewModels = 
                _mapper.Map<List<Listing>, List<ListingViewModel>>
                    (listings.ToList());

            return viewModels ?? new List<ListingViewModel>();
        }

        public async Task<IEnumerable<ListingViewModel>> GetFilteredListingViewModels(int status, string query)
        {
            var listings = await _listingRepository.GetAllListings();

            if (listings == null || !listings.Any())
            {
                return new List<ListingViewModel>();
            }
            
            IEnumerable<Listing> filteredListings;

            if (!string.IsNullOrEmpty(query))
            {
                filteredListings = listings.Where(m => m.Status.Id == status && m.Car.Make.Contains(query));
            }
            else
            {
                filteredListings = listings.Where(m => m.Status.Id == status);
            }

            var enumeratedListings = filteredListings.ToList();
            
            if (enumeratedListings.Any())
            {
                var viewModels = _mapper.Map<List<Listing>, List<ListingViewModel>>(enumeratedListings.ToList());
                
                return viewModels;
            }

            return new List<ListingViewModel>();
        }

        public async Task<Listing> GetListingByIdAsync(int id)
        {
            var listing = await _listingRepository.GetListingById(id);

            return listing;
        }

        public async Task<ListingInputModel> GetListingInputModelByIdAsync(int id)
        {
            var listing = await _listingRepository.GetListingById(id);

            if (listing == null) return null;
            
            var inputModel = _mapper.Map<ListingInputModel>(listing);

            return inputModel;
        }

        public async Task<ListingViewModel> GetListingViewModelByIdAsync(int id)
        {
            var listing = await _listingRepository.GetListingById(id);

            if (listing == null) return null;
            
            var viewModel = _mapper.Map<ListingViewModel>(listing);

            return viewModel;
        }

        public async Task AddListingAsync(ListingInputModel inputModel)
        {
            if (inputModel != null)
            {

                var listing = new Listing
                {
                    Car = new Car(),
                    RepairJob = new RepairJob()
                };
                
                _listingRepository.TrackListing(listing);
                await MapListingValues(inputModel, listing);
                _carService.MapCarValues(inputModel.Car, listing.Car);
                _repairJobService.MapRepairJobValues(inputModel.RepairJob, listing.RepairJob);
                _mediaService.UpdateMediaCollection(inputModel.ImgNames, listing);

                listing.DateCreated = DateTime.Today;
                listing.DateLastUpdated = DateTime.Today;
                
                _listingRepository.AddListing(listing);
            }
        }

        public async Task<bool> UpdateListingAsync(ListingInputModel source)
        {
            if (source == null)
            {
                return false;
            }

            try
            {
                var entity = await GetListingByIdAsync(source.Id);
                if (entity == null) return false;
                
                await MapListingValues(source, entity);
                _carService.MapCarValues(source.Car, entity.Car);
                _repairJobService.MapRepairJobValues(source.RepairJob, entity.RepairJob);
                _mediaService.UpdateMediaCollection(source.ImgNames, entity);
                
                entity.DateLastUpdated = DateTime.Today;
                
                _listingRepository.UpdateListing(entity);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private async Task MapListingValues(ListingInputModel inputModel, Listing entity)
        {
            if (entity == null)
            {
                throw new Exception("Listing entity not found");
            }
            
            var status = await _statusService.GetStatusByIdAsync(inputModel.Status);

            if (status == null)
            {
                throw new Exception("Status entity not found.");
            }
            
            var listingEntityEntry = _listingRepository.GetListingEntityEntry(entity);

            listingEntityEntry.CurrentValues.SetValues(inputModel);
            
            entity.Status = status;
        }
    }
}