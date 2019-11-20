using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheCarHub.Models;
using TheCarHub.Models.Entities;
using TheCarHub.Models.InputModels;
using TheCarHub.Models.ViewModels;
using TheCarHub.Services;

namespace TheCarHub.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ListingController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IListingService _listingService;
        private readonly ICarService _carService;
        private readonly IStatusService _statusService;
        private readonly IMediaService _mediaService;
        private readonly IMapper _mapper;

        public ListingController(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment,
            IListingService listingService,
            ICarService carService,
            IStatusService statusService,
            IMediaService mediaService,
            IMapper mapper)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _listingService = listingService;
            _carService = carService;
            _statusService = statusService;
            _mediaService = mediaService;
            _mapper = mapper;
        }

        // GET: Listing
        public async Task<IActionResult> Index()
        {
            var listings = await _listingService.GetAllListings();

            var viewModels = new List<ListingViewModel>();

            foreach (var item in listings)
            {
                viewModels
                    .Add(_mapper.Map<ListingViewModel>(item));
            }

            return View(viewModels);
        }

        // GET: Listing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing =
                await _listingService.GetListingById(id.GetValueOrDefault());

            var viewModel = _mapper.Map<ListingViewModel>(listing);


            if (listing == null || viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Listing/Create
        public IActionResult Create()
        {
//            var listings =
//                await _listingService.GetAllListings();
//
//            var filteredListings =
//                listings
//                    .Where(i => i.Status.Id == 1)
//                    .Select(i => i.Car);
//
//            var carSelect =
//                filteredListings
//                    .Select(i =>
//                        new
//                        {
//                            Id = i.Id,
//                            Description = $"{i.Year.Year} {i.Make} {i.Model} {i.Trim} ({i.VIN})"
//                        }
//                    );
//
//            ViewData["NewCars"] =
//                new SelectList(carSelect,
//                    "Id",
//                    "Description");
            
            var years = new List<int>();

            for (int i = 1990; i <= (DateTime.Today.Year + 1); i++)
            {
                years.Add(i);
            }

            var yearSelect =
                years
                    .Select(i =>
                        new
                        {
                            Value = i,
                            Text = i.ToString()
                        });

            ViewData["YearSelect"] =
                new SelectList(yearSelect,
                    "Value",
                    "Text");

            return View();
        }
        
        // POST: Listing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListingInputModel inputModel)
        {
            if (ModelState.IsValid)
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
                    Status = await _statusService.GetStatusByName("available"),
                    DateCreated = DateTime.Today,
                    DateLastUpdated = DateTime.Today,
                    PurchaseDate = inputModel.PurchaseDate,
                    SellingPrice = inputModel.PurchasePrice 
                };

//                car.Listings.Add(listing);
//                var listing = await _listingService.GetById(viewModel.Id);
//
//                if (listing == null)
//                {
//                    return NotFound();
//                }

                var mediaList = new List<Media>();

                // Image upload
                if (inputModel.Files != null && inputModel.Files.Count > 0)
                {
                    foreach (var file in inputModel.Files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName =
                                Path.GetRandomFileName() + Path.GetExtension(file.FileName);

                            var path =
                                Path.Combine(_webHostEnvironment.WebRootPath,
                                    _configuration["Media:Directory"], $"{fileName}");

                            var media = new Media
                            {
                                FileName = fileName,
                                Listing = listing,
                                Caption = "",
//                              MediaTags = new List<MediaTag>()
                                // TODO: Tags
                            };
                            
                            listing.Media.Add(media);

                            using (var stream = System.IO.File.Create(path))
                            {
                                await file.CopyToAsync(stream);
                            }
                            
//                            _mediaService.AddMedia(media);
                        }
                    }
                }
                
                _listingService.AddListing(listing);
//                _carService.AddCar(car);
                
                return RedirectToAction(nameof(Index));
            }

            var years = new List<int>();

            for (int i = 1990; i <= (DateTime.Today.Year + 1); i++)
            {
                years.Add(i);
            }

            var yearSelect =
                years
                    .Select(i =>
                        new
                        {
                            Value = i,
                            Text = i.ToString()
                        });

            ViewData["YearSelect"] =
                new SelectList(yearSelect,
                    "Value",
                    "Text");
            
            return View(inputModel);
        }
        
        // POST: Listing/Create/MediaUpload
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> MediaUpload([Bind("Files")] MediaInputModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var listing = await _listingService.GetById(viewModel.Id);
//
//                if (listing == null)
//                {
//                    return NotFound();
//                }
//
//                // Image upload
//                if (model.Files != null && model.Files.Count > 0)
//                {
//                    foreach (var file in model.Files)
//                    {
//                        if (file.Length > 0)
//                        {
//                            var fileName =
//                                Path.GetRandomFileName() + Path.GetExtension(file.FileName);
//
//                            var path =
//                                Path.Combine(_webHostEnvironment.WebRootPath,
//                                    _configuration["Media:Directory"], $"{fileName}");
//
//                            if (listing.Media == null)
//                            {
//                                listing.Media = new List<Media>();
//                            }
//
//                            listing.Media.Add(new Media
//                            {
//                                FileName = fileName,
//                                ListingId = listing.Id,
//                                Listing = listing,
//                                Caption = "",
//                                MediaTags = new List<MediaTag>()
//                            });
//
//                            using (var stream = System.IO.File.Create(path))
//                            {
//                                await file.CopyToAsync(stream);
//                            }
//                        }
//                    }
//                }
//        }

        // GET: Listing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = 
                await _listingService.GetListingById(id.GetValueOrDefault());

            if (listing == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ListingViewModel>(listing);

            ViewData["CarId"] = new SelectList(
                await _carService.GetAllCars(),
                "Id",
                "Id",
                viewModel.CarId);

            return View(viewModel);
        }

        // POST: Listing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind(
                "Id, Title,CarId,Description,Status,DateCreated," +
                "DateLastUpdated,PurchaseDate,PurchasePrice,SellingPrice," +
                "SaleDate")]
            ListingViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var listing = await _listingService.GetListingById(id);

                listing.Title = viewModel.Title;
                listing.CarId = viewModel.CarId;
                listing.Description = viewModel.Description;
//                listing.Status = viewModel.Status;
                listing.DateCreated = viewModel.DateCreated;
                listing.DateLastUpdated = DateTime.Today;
                listing.PurchaseDate = viewModel.PurchaseDate;
                listing.SellingPrice = viewModel.SellingPrice;
                listing.SaleDate = viewModel.SaleDate;

                try
                {
                    _listingService.EditListing(listing);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var listingExists = await ListingExists(viewModel.Id);

                    if (!listingExists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CarId"] = new SelectList(
                await _carService.GetAllCars(),
                "Id",
                "Id",
                viewModel.CarId);

            return View(viewModel);
        }

        // GET: Listing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _listingService.GetListingById(id.GetValueOrDefault());

            if (listing == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ListingViewModel>(listing);

            return View(viewModel);
        }

        // POST: Listing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listing = await _listingService.GetListingById(id);

            _listingService.DeleteListing(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ListingExists(int id)
        {
            var listings = await _listingService.GetAllListings();

            return listings.Any(e => e.Id == id);
        }
    }
}