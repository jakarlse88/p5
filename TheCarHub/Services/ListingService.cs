using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheCarHub.Models;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;
using TheCarHub.Models.Validators;
using TheCarHub.Repositories;

namespace TheCarHub.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IMappingService<ListingInputModel, Listing> _mappingService;
        private readonly IMapper _mapper;

        public ListingService(IListingRepository listingRepository, 
            IStatusRepository statusRepository,
            IMapper mapper,
            IMappingService<ListingInputModel, Listing> mappingService)
        {
            _listingRepository = listingRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
            _mappingService = mappingService;
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

        public void EditListing(ListingInputModel inputModel, Listing listing)
        {
            if (inputModel != null && listing != null)
            {
//                // Car details
//                listing.Car.VIN = inputModel.Car.VIN;
//                listing.Car.Year = new DateTime(inputModel.CarYear, 1, 1);
//                listing.Car.Make = inputModel.Car.Make;
//                listing.Car.Model = inputModel.Car.Model;
//                listing.Car.Trim = inputModel.Car.Trim;
//                
//                // Listing details
//                listing.Title = inputModel.Title;
//                listing.Description = inputModel.Description;
//                listing.Status = await _statusRepository.GetStatusByIdAsync(int.Parse(inputModel.Status));
//                listing.SaleDate = listing.Status.Id == 2 ? inputModel.SaleDate : listing.SaleDate; 
//                listing.DateCreated = inputModel.DateCreated;
//                listing.DateLastUpdated = DateTime.Today;
//                listing.PurchaseDate = inputModel.PurchaseDate;
//                listing.PurchasePrice = inputModel.PurchasePrice;
//                listing.SellingPrice = inputModel.SellingPrice;
//                
//                // Media details
//                
//                // RepairJob details
//                listing.RepairJob.Cost = inputModel.RepairJob.Cost;
//                listing.RepairJob.Description = inputModel.RepairJob.Description;

                _mappingService.Map(inputModel, listing);

                _listingRepository.EditListing(listing);
            }
        }

        public async Task AddListingAsync(ListingInputModel inputModel)
        {
            if (inputModel != null)
            {
                var listing = await _mappingService.Map(inputModel);
                
                _listingRepository.AddListing(listing);
            }
        }

        public void ValidateListingInputModel(ModelStateDictionary modelState, 
            ListingInputModel inputModel)
        {
            var validator = new ListingInputModelValidator();
            
            var results = validator.Validate(inputModel);
            
            results.AddToModelState(modelState, null);    
        }

        public void DeleteListing(int id)
        {
            _listingRepository.DeleteListing(id);
        }
    }
}