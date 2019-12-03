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
        private readonly IMappingService<ListingInputModel, Listing> _mappingService;

        public ListingService(IListingRepository listingRepository, 
            IMappingService<ListingInputModel, Listing> mappingService)
        {
            _listingRepository = listingRepository;
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

        public async Task EditListing(ListingInputModel inputModel, Listing listing)
        {
            if (inputModel != null && listing != null)
            {
                await _mappingService.Map(inputModel, listing);

                _listingRepository.EditListing(listing);
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

        public void ValidateListingInputModel(
            ModelStateDictionary modelState, 
            ListingInputModel inputModel)
        {
            if (modelState == null)
            {
                return;
            }
            
            if (inputModel == null)
            {
                modelState.AddModelError(
                    "InputModelNull",
                    "Input model cannot be null.");

                return;
            }
            
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