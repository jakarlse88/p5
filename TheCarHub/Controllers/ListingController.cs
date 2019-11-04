using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Data;
using TheCarHub.Models.Entities;
using TheCarHub.Models.ViewModels;
using TheCarHub.Services;

namespace TheCarHub.Controllers
{
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
            var listings = await _listingService.GetAll();

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
                await _listingService.GetById(id.GetValueOrDefault());

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
                "Id,Title,CarId,Description,Status,DateCreated," +
                "DateLastUpdated,PurchaseDate,PurchasePrice,SellingPrice," +
                "SaleDate")]
            ListingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var listing = _mapper.Map<Listing>(viewModel);

                _listingService.Add(listing);

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

            var listing = await _listingService.GetById(id.GetValueOrDefault());

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
                "Id,Title,CarId,Description,Status,DateCreated," +
                "DateLastUpdated,PurchaseDate,PurchasePrice,SellingPrice," +
                "SaleDate")]
            ListingViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            var listing = _mapper.Map<Listing>(viewModel);
            
            if (ModelState.IsValid)
            {
                try
                {
                    _listingService.Edit(listing);
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

            var listing = await _listingService.GetById(id.GetValueOrDefault());
            
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
            var listing = await _listingService.GetById(id);
            
            _listingService.Delete(id);
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ListingExists(int id)
        {
            var listings = await _listingService.GetAll();
            
            return listings.Any(e => e.Id == id); 
        }
    }
}