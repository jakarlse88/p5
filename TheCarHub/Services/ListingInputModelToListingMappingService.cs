using System;
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
    public class ListingInputModelToListingMappingService : IMappingService<ListingInputModel, Listing>
    {
        private readonly IStatusRepository _statusRepository;

        public ListingInputModelToListingMappingService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        private Car MapCarInputModelToCar(in CarInputModel source, int carYear, Car destination)
        {
            if (source == null)
                return null;

//            var success = int.TryParse(.ToString(), out int carYearMax);
//
//            if (!success) return null;

            carYear = (carYear >= 1990 && carYear <= DateTime.Now.AddYears(1).Year) ? carYear : 2019;

            destination ??= new Car();

            destination.VIN = source.VIN;
            destination.Year = new DateTime(carYear, 1, 1);
            destination.Make = source.Make;
            destination.Model = source.Model;
            destination.Trim = source.Trim;

            return destination;
        }

        private RepairJob MapRepairJobInPutModelToRepairJob(in RepairJobInputModel source, RepairJob destination)
        {
            if (source == null)
                return null;

            destination ??= new RepairJob();

            destination.Cost = source.Cost;
            destination.Description = source.Description;

            return destination;
        }

        private ICollection<Media> MapImgNamesToMedia(IEnumerable<string> imgNames)
        {
            var media = new HashSet<Media>();

            if (imgNames == null)
                return media;

            var enumerable = imgNames.ToList();

            if (enumerable.Any())
            {
                foreach (var name in enumerable)
                {
                    media.Add(new Media
                    {
                        FileName = name,
                        Tags = new List<MediaTag>()
                    });
                }
            }

            return media;
        }

        private void AddMediaEntitiesToListing(IEnumerable<Media> media, Listing listing)
        {
            foreach (var item in media)
            {
                item.Listing = listing;
            }
        }

        /// <summary>
        /// Maps the properties of a ListingInputModel object onto a new
        /// Listing object.
        /// </summary>
        /// <param name="source">Source ListingInputModel object, from which properties are mapped.</param>
        /// <returns>A populated Listing object.</returns>
        public async Task<Listing> Map(ListingInputModel source)
        {
            if (source?.Car == null || source.RepairJob == null)
                return null;

            var destination = new Listing();

            var car = MapCarInputModelToCar(source.Car, source.CarYear, destination.Car);
            var repairJob = MapRepairJobInPutModelToRepairJob(source.RepairJob, destination.RepairJob);
            var media = MapImgNamesToMedia(source.ImgNames);

            destination.Title = source.Title;
            destination.Description = source.Description;
            destination.Status = await _statusRepository.GetStatusByIdAsync(1);
            destination.DateCreated = DateTime.Today;
            destination.DateLastUpdated = DateTime.Today;
            destination.PurchaseDate = source.PurchaseDate;
            destination.PurchasePrice = source.PurchasePrice;
            destination.SellingPrice = source.SellingPrice;

            destination.Car = car;
            destination.RepairJob = repairJob;
            destination.Media = media;

            if (media.Count > 0)
            {
                AddMediaEntitiesToListing(media, destination);
            }

            car.Listings.Add(destination);

            return destination;
        }

        /// <summary>
        /// Maps the properties of a ListingInputModel object onto an already
        /// extant Listing object, overriding the latter's properties.
        /// </summary>
        /// <param name="source">Source ListingInputModel object, from which properties are mapped.</param>
        /// <param name="destination">Destination Input object, onto which properties are mapped.</param>
        /// <returns></returns>
        public async Task<Listing> Map(ListingInputModel source, Listing destination)
        {
            var car = MapCarInputModelToCar(source.Car, source.CarYear, destination.Car);
            var repairJob = MapRepairJobInPutModelToRepairJob(source.RepairJob, destination.RepairJob);

            var filteredImgNames = source.ImgNames.Where(i => destination.Media.All(m => m.FileName != i));
            
            var media = MapImgNamesToMedia(filteredImgNames);

            if (car == null || repairJob == null)
                return null;

            // Listing details
            destination.Title = source.Title;
            destination.Description = source.Description;
            destination.Status = await _statusRepository.GetStatusByIdAsync(int.Parse(source.Status));
            destination.SaleDate = destination.Status.Id == 2 ? source.SaleDate : destination.SaleDate;
            destination.DateCreated = source.DateCreated;
            destination.DateLastUpdated = DateTime.Today;
            destination.PurchaseDate = source.PurchaseDate;
            destination.PurchasePrice = source.PurchasePrice;
            destination.SellingPrice = source.SellingPrice;

            // RepairJob details
            destination.RepairJob.Cost = source.RepairJob.Cost;
            destination.RepairJob.Description = source.RepairJob.Description;
            
            destination.Car = car;
            destination.RepairJob = repairJob;
            destination.Media = media;

            if (media.Count > 0)
            {
                AddMediaEntitiesToListing(media, destination);
            }

            return destination;
        }
    }
}