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
            var carYearSelect = PopulateCarYearSelect();

            ViewData["YearSelect"] =
                new SelectList(carYearSelect,
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
//                var car = new Car
//                {
//                    VIN = inputModel.Car.VIN,
//                    Year = new DateTime(inputModel.CarYear, 1, 1),
//                    Make = inputModel.Car.Make,
//                    Model = inputModel.Car.Model,
//                    Trim = inputModel.Car.Trim
//                };
//
//                var listing = new Listing
//                {
//                    Title = inputModel.Title,
//                    Car = car,
//                    Description = inputModel.Description,
//                    Status = await _statusService.GetStatusByName("available"),
//                    DateCreated = DateTime.Today,
//                    DateLastUpdated = DateTime.Today,
//                    PurchaseDate = inputModel.PurchaseDate,
//                    SellingPrice = inputModel.PurchasePrice
//                };
//
//
//                foreach (var name in inputModel.ImgNames)
//                {
//                    listing.Media.Add(new Media
//                    {
//                        FileName = name,
//                        Listing = listing,
//                        Caption = "",
//                        Tags = new List<MediaTag>()
//                    });
//                }

//                
                await _listingService.AddListing(inputModel);

                return RedirectToAction(nameof(Index));
            }

            var carYearSelect = PopulateCarYearSelect();

            ViewData["YearSelect"] =
                new SelectList(carYearSelect,
                    "Value",
                    "Text");

            return View(inputModel);
        }

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
            [Bind("Id, Title,CarId,Description,Status,DateCreated," +
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

        /// <summary>
        /// Utility class for car year SelectList creation.
        /// </summary>
        private class YearSelectItem
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

        /// <summary>
        /// Utility method that populates a list with YearSelectItems
        /// representing years ranging from 1990 to current year + 1.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<YearSelectItem> PopulateCarYearSelect()
        {
            var years = new List<YearSelectItem>();

            for (int i = 1990; i <= (DateTime.Today.Year + 1); i++)
            {
                years.Add(new YearSelectItem
                {
                    Value = i,
                    Text = i.ToString()
                });
            }

            return years;
        }
    }
}