using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using TheCarHub.Models;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;
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
        private readonly IMappingService<ListingInputModel, Listing> _mappingService;

        public ListingService(IListingRepository listingRepository,
            IRepairJobService repairJobService,
            ICarService carService,
            IStatusService statusService,
            IMediaService mediaService,
            IMappingService<ListingInputModel, Listing> mappingService)
        {
            _listingRepository = listingRepository;
            _repairJobService = repairJobService;
            _carService = carService;
            _statusService = statusService;
            _mediaService = mediaService;
            _mappingService = mappingService;
        }

        public async Task<IEnumerable<Listing>> GetAllListings()
        {
            var results = await _listingRepository.GetAllListings();

            return results;
        }

        public async Task<Listing> GetListingByIdAsync(int id)
        {
            var listing = await _listingRepository.GetListingById(id);

            return listing;
        }

        public async Task EditListing(ListingInputModel inputModel, Listing listing)
        {
            if (inputModel != null && listing != null)
            {
                await _mappingService.Map(inputModel, listing);

                _listingRepository.UpdateListing(listing);
            }
        }

        public async Task AddListingAsync(ListingInputModel inputModel)
        {
            if (inputModel != null)
            {
                var listing = await _mappingService.Map(inputModel);

                if (listing == null)
                    return;

                _listingRepository.AddListing(listing);
            }
        }

        public void DeleteListing(int id)
        {
            _listingRepository.DeleteListing(id);
        }

        public async Task<bool> UpdateListingExperimentalAsync(ListingInputModel source)
        {
            if (source == null)
            {
                return false;
            }

            var entity = await GetListingByIdAsync(source.Id);

            if (entity == null)
            {
                return false;
            }

            var entry = _listingRepository.GetListingEntityEntry(entity);

            entry.CurrentValues.SetValues(source);

            await _carService.UpdateCarExperimentalAsync(source.Car);

            await _repairJobService.UpdateRepairJobAsync(source.RepairJob);

            entity.Status =
                await _statusService.GetStatusByNameAsync(source.Status);

            _mediaService.UpdateMediaExperimental(source.ImgNames, entity);

            _listingRepository.UpdateListing(entity);

            return true;
        }
    }
}