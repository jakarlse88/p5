using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCarHub.Models;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;
using TheCarHub.Repositories;

namespace TheCarHub.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IStatusRepository _statusRepository;

        public ListingService(IListingRepository listingRepository, IStatusRepository statusRepository)
        {
            _listingRepository = listingRepository;
            _statusRepository = statusRepository;
        }

        public async Task<IEnumerable<Listing>> GetAllListings()
        {
            var results = await _listingRepository.GetAllListings();

            return results;
        }

        public async Task<Listing> GetListingById(int id)
        {
            var listing = await _listingRepository.GetListingById(id);

            return listing;
        }

        public void EditListing(Listing listing)
        {
            if (listing != null)
            {
                _listingRepository.EditListing(listing);
            }
        }

        public async Task AddListing(ListingInputModel inputModel)
        {
            if (inputModel != null)
            {
                var car = new Car
                {
                    VIN = inputModel.Car.VIN,
                    Year = new DateTime(inputModel.CarYear, 1, 1),
                    Make = inputModel.Car.Make,
                    Model = inputModel.Car.Model,
                    Trim = inputModel.Car.Trim
                };

                var listing = new Listing
                {
                    Title = inputModel.Title,
                    Car = car,
                    Description = inputModel.Description,
                    Status = await _statusRepository.GetStatusByName("available"),
                    DateCreated = DateTime.Today,
                    DateLastUpdated = DateTime.Today,
                    PurchaseDate = inputModel.PurchaseDate,
                    SellingPrice = inputModel.PurchasePrice
                };

                foreach (var name in inputModel.ImgNames)
                {
                    listing.Media.Add(new Media
                    {
                        FileName = name,
                        Listing = listing,
                        Caption = "",
                        Tags = new List<MediaTag>()
                    });
                }
                
                _listingRepository.AddListing(listing);
            }
        }

        public void DeleteListing(int id)
        {
            _listingRepository.DeleteListing(id);
        }
    }
}