using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Models.Entities;
using TheCarHub.Models.ViewModels;
using TheCarHub.Services;

namespace TheCarHub.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ListingController : Controller
    {
        private readonly IListingService _listingService;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public ListingController(
            IListingService listingService,
            ICarService carService,
            IMapper mapper)
        {
            _listingService = listingService;
            _carService = carService;
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
        public async Task<IActionResult> Create()
        {
            var cars = await _carService.GetAllCars();

            ViewData["CarId"] =
                new SelectList(cars, "Id", "Id");

            return View();
        }

        // POST: Listing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "Title,CarId,Description,Status," +
                "DateLastUpdated,PurchaseDate,PurchasePrice,SellingPrice," +
                "SaleDate")]
            ListingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var listing = new Listing
                {
                    Title = viewModel.Title,
                    CarForeignKey = viewModel.CarId,
                    Description = viewModel.Description,
                    Status = viewModel.Status,
                    DateCreated = DateTime.Today,
                    DateLastUpdated = DateTime.Today,
                    PurchaseDate = viewModel.PurchaseDate,
                    SellingPrice = viewModel.SellingPrice,
                    SaleDate = viewModel.SaleDate
                };

                _listingService.AddListing(listing);

                return RedirectToAction(nameof(Index));
            }

            ViewData["CarId"] = new SelectList(await _carService.GetAllCars(), "Id", "Id", viewModel.CarId);
            return View(viewModel);
        }

        // GET: Listing/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            ViewData["CarId"] = new SelectList(await _carService.GetAllCars(), "Id", "Id", viewModel.CarId);

            return View(viewModel);
        }

        // POST: Listing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind(
                "Title,CarId,Description,Status,DateCreated," +
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
                listing.CarForeignKey = viewModel.CarId;
                listing.Description = viewModel.Description;
                listing.Status = viewModel.Status;
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

            ViewData["CarId"] = new SelectList(await _carService.GetAllCars(), "Id", "Id", viewModel.CarId);
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