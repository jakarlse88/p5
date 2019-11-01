using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheCarHub.Models;
using TheCarHub.Services;
using System.Linq;
using System;
using AutoMapper;
using System.Net;

namespace TheCarHub.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ICarService _carService;
        private readonly IListingService _listingService;
        private readonly IMapper _mapper;

        public AdminController(ILogger<AdminController> logger,
            ICarService carService,
            IListingService listingService,
            IMapper mapper)
        {
            _logger = logger;
            _carService = carService;
            _listingService = listingService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllCars();
            var listings = await _listingService.GetAllListings();

            var model = new AdminViewModel
            {
                Cars = cars.ToList(),
                Listings = listings.ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Listing(int id)
        {
            // if (id == null)
            // {
            //     return new BadRequestResult();
            // }

            var model = await _listingService.GetListingById(id);

            if (model != null)
            {
                var viewModel = _mapper.Map<ListingViewModel>(model);

                return View(viewModel);
            }

            return NotFound();
        }

        // [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditListing(ListingViewModel viewModel)
        {
            var listing = await _listingService.GetListingById(viewModel.Id);

            if (listing == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                listing.Title = viewModel.Title;

                _listingService.UpdateListing(listing);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}